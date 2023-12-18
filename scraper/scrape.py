"""Crawl the EA SPORTS database."""

import numpy as np
import requests
from bs4 import BeautifulSoup as bs
import re
import csv

MALE_CSV   = "csv/male_players.csv"
FEMALE_CSV = "csv/female_players.csv"

fields = ['Name', 'Nation', 'Club', 'League',
         'Position', 'Age', 'Height', 'Weight',
         'Overall', 'Pace', 'Shooting', 'Passing',
         'Dribbling', 'Defending', 'Physicality', 'Acceleration',
         'Sprint', 'Positioning', 'Finishing', 'Shot', 'Long',
         'Volleys', 'Penalties', 'Vision', 'Crossing', 'Free',
         'Curve', 'Agility', 'Balance', 'Reactions', 'Ball',
         'Composure', 'Interceptions', 'Heading', 'Def', 'Standing',
         'Sliding', 'Jumping', 'Stamina', 'Strength', 'Aggression',
         'Att work rate', 'Def work rate', 'Preferred foot', 'Weak foot',
         'Skill moves', 'URL', 'Gender', 'GK']

def scrape_players(gender):
    pages = 16 if gender else 160
    count = 0

    csv_file = FEMALE_CSV if gender else MALE_CSV

    with open(csv_file, 'w') as fd:
        writer = csv.writer(fd)
        writer.writerow(fields)

    for page in range(1, pages):
        absolute = 'https://www.ea.com'
        url = f'https://www.ea.com/games/ea-sports-fc/ratings?gender={gender}&page={page}'
        html = requests.get(url)
        html.status_code == requests.codes.ok
        soup = bs(html.text)
        
        table = soup.find( 'tbody', class_ = 'Table_tbody__gYqSw')
        for link in table.findAll('a', class_ = 'Table_profileCellAnchor__VU0JH'):
            count += 1

            player_link = absolute+link['href']

            player_dict = {}
            player_html = requests.get(player_link)
            player_soup = bs(player_html.text)

            # Club and national team
            teams = []
            for item in player_soup.findAll(class_ ='Picture_responsiveImageWrap__XmvLe'):
                team = item.img['alt']
                if team == '':
                    continue
                else:
                    teams.append(team)
            try:
                national_team = teams[0]
            except:
                national_team = np.nan
            try:
                club = teams[1]
            except:
                club = np.nan

            # League
            league = player_soup.find(string = 'LEAGUE').parent.text[6:]

            # Age
            try:
                age = int(player_soup.find(string = 'AGE').parent.text[3:])
            except:
                age = np.nan

            # Height
            height = player_soup.find(string = 'HEIGHT').parent.text[6:9]

            # Weight - assume under 100kg, deal with exceptions manually
            weight = player_soup.find(string = "WEIGHT").parent.text[6:8]

            # Get overall
            overall = player_soup.find(class_='Table_statCellValue____Twu').text

            # Get player position
            position = player_soup.find(class_='Table_tag__3Mxk9 generated_utility3sm__0pg6W generated_utility1lg__ECKe_').text

            # Name of player
            for stats in player_soup.findAll(class_ = 'Table_profileCellAnchor__VU0JH'):
                player_dict['Name'] = stats.text
            
            player_dict.update({
                'Nation': national_team, 
                'Club': club,
                'League': league,
                'Position':position, 
                'Age':age,
                'Height': height,
                'Weight': weight,
                'Overall':overall 
            })

            # Player general stats   
            for stats in player_soup.findAll('div', class_ ='Stat_stat__lh90p generated_utility2__1zAUs'):
                try:
                    player_dict[re.search('[A-Za-z]+', stats.text)[0]] = int(re.search('[0-9]+', stats.text)[0])
                except:
                    player_dict[re.search('[A-Za-z]+', stats.text)[0]] = np.nan

            # All player stats
            for stats in player_soup.findAll(class_='Stat_stat__lh90p Stat_bar__hVgdN generated_utility3__mFgLe'):
                player_dict[re.search('[A-Za-z]+', stats.text)[0]] = int(re.search('[0-9]+', stats.text)[0])

            # Attacking workrate
            att_work_rate = player_soup.find(string = 'ATT WORK RATE').parent.text[13:]

            # Defensive workrate
            def_work_rate = player_soup.find(string = 'DEF WORK RATE').parent.text[13:]

            # Preferred foot
            preferred_foot = player_soup.find(string = 'PREFERRED FOOT').parent.text[14:]

            # Weak foot
            weak_foot = player_soup.find(string = 'WEAK FOOT').parent.span['aria-label'][0]

            # Skill moves
            skill_moves = player_soup.find(string = 'SKILL MOVES').parent.span['aria-label'][0]

            player_dict.update({
                'Att work rate':att_work_rate,
                'Def work rate':def_work_rate,
                'Preferred foot':preferred_foot,
                'Weak foot': weak_foot,
                'Skill moves':skill_moves,
                'URL':player_link,
                'Gender': 'F' if gender else 'M'
            })

            if 'Goalkeeping' in player_dict:
                player_dict.pop('Goalkeeping')

            print(count, player_dict['Name'])

            with open(csv_file, 'a') as fd:
                writer = csv.DictWriter(fd, fieldnames=fields)
                writer.writerow(player_dict)

def main():
    scrape_players(gender=0)
    scrape_players(gender=1)

if __name__ == '__main__':
    main()