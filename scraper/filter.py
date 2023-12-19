import csv

def filter_file(source, target, league):
    with open(source, 'r') as infile, open(target, 'w') as outfile:
        reader = csv.DictReader(infile)
        fieldnames = reader.fieldnames

        writer = csv.DictWriter(outfile, fieldnames=fieldnames)
        writer.writeheader()

        for row in reader:
            if (row['League'] == league):
                writer.writerow(row)

if __name__ == '__main__':
    SOURCE_FILE = "csv/male_players.csv"
    TARGET_FILE = "csv/premier_league.csv"
    LEAGUE = "Premier League"

    SOURCE_FILE2 = "csv/female_players.csv"
    TARGET_FILE2 = "csv/womens_super_league.csv"
    LEAGUE2 = "Barclays WSL"

    # filter_file(SOURCE_FILE, TARGET_FILE, LEAGUE)
    filter_file(SOURCE_FILE2, TARGET_FILE2, LEAGUE2)
