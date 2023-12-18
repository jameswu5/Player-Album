URL_START = "https://cdn.sofifa.net/players/"
URL_END = "/24_120.png"

def get_image(player_id:int):
    part1 = "{:0>3}".format(str(player_id // 1000))
    part2 = "{:0>3}".format(str(player_id % 1000))
    return URL_START + part1 + '/' + part2 + URL_END