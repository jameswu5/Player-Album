from database import Database
from read import read_file

PREM_PATH = "static/csv/premier_league.csv"
WSL_PATH  = "static/csv/womens_super_league.csv"

if __name__ == '__main__':
    db = Database()
    db.create_tables()
    db.populate_players(read_file(PREM_PATH))
