import requests
from PIL import Image
from io import BytesIO
import csv

URL_START = "https://cdn.sofifa.net/players/"
URL_END = "/24_120.png"

TARGET_PATH = "static/faces/premier-league/"
WSL_CSV = "static/csv/womens_super_league.csv"
PREM_CSV = "static/csv/premier_league.csv"

def get_url(player_id:int):
    part1 = "{:0>3}".format(str(player_id // 1000))
    part2 = "{:0>3}".format(str(player_id % 1000))
    return URL_START + part1 + '/' + part2 + URL_END

def get_target_path(player_id):
    return f"{TARGET_PATH}{player_id}.png"

def download_image(url, target_path):
    response = requests.get(url)
    if response.status_code == 200:
        image = Image.open(BytesIO(response.content))
        image.save(target_path)
    else:
        print(f"Failed to download image. Status code: {response.status_code}")

def download_faces(csv_path):
    with open(csv_path, 'r') as f:
        reader = csv.DictReader(f)
        for row in reader:
            player_id = int(row['ID'])
            download_image(get_url(player_id), get_target_path(player_id))
            print(row['Name'])

if __name__ == '__main__':
    download_faces(PREM_CSV)