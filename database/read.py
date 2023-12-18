import csv

MALE_PATH = "csv/male_players.csv"
FEMALE_PATH = "csv/female_players.csv"

def read_file(file_path):
    with open(file_path.format(tuple), encoding="utf8") as csvfile:
        location_reader = csv.DictReader(csvfile)
        players = [row for row in location_reader]
        return players

if __name__ == '__main__':
    print(read_file(MALE_PATH)[0])