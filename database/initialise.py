from database import Database
from read import read_file

MALE_PATH = "csv/male_players.csv"
FEMALE_PATH = "csv/female_players.csv"

if __name__ == '__main__':
    db = Database()
    db.create_tables()
    db.populate_players(read_file(MALE_PATH))
    db.populate_players(read_file(FEMALE_PATH))
