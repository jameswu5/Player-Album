import csv

def read_file(file_path):
    with open(file_path.format(tuple), encoding="utf8") as csvfile:
        location_reader = csv.DictReader(csvfile)
        players = [row for row in location_reader]
        return players

if __name__ == '__main__':
    pass