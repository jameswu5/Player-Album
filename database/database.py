import sqlite3

DATABASE_PATH = "database/players.db"

class Database:
    def __init__(self):
        self.__conn = sqlite3.connect(DATABASE_PATH)
        self.__cur = self.__conn.cursor()

    def create_tables(self) -> None:
        self.__cur.execute(
            """
            CREATE TABLE IF NOT EXISTS Player (
                ID INTEGER PRIMARY KEY,
                Club TEXT NOT NULL,
                League TEXT NOT NULL,
                Nation TEXT NOT NULL,
                Name TEXT NOT NULL,
                Position TEXT NOT NULL,
                Age INTEGER NOT NULL,
                Height INTEGER NOT NULL,
                Weight INTEGER NOT NULL,
                Overall INTEGER NOT NULL,
                Pace INTEGER NOT NULL,
                Shooting INTEGER NOT NULL,
                Passing INTEGER NOT NULL,
                Dribbling INTEGER NOT NULL,
                Defending INTEGER NOT NULL,
                Physicality INTEGER NOT NULL,
                DefensiveWorkRate TEXT NOT NULL,
                AttackingWorkRate TEXT NOT NULL,
                PreferredFoot CHAR NOT NULL,
                WeakFoot INTEGER NOT NULL,
                SkillMoves INTEGER NOT NULL,
                Gender CHAR NOT NULL
            );
            """
        )

        self.__conn.commit()

    def populate_players(self, players: list) -> None:
        for player in players:
            # Insert the player
            query = (
                f"""
                INSERT INTO Player (
                    ID,
                    Club,
                    League,
                    Nation,
                    Name,
                    Position,
                    Age,
                    Height,
                    Weight,
                    Overall,
                    Pace,
                    Shooting,
                    Passing,
                    Dribbling,
                    Defending,
                    Physicality,
                    DefensiveWorkRate,
                    AttackingWorkRate,
                    PreferredFoot,
                    WeakFoot,
                    SkillMoves,
                    Gender
                )
                VALUES (
                    {player["ID"]},
                    "{player['Club']}",
                    "{player['League']}",
                    "{player['Nation']}",
                    "{player["Name"]}",
                    "{player["Position"]}",
                    {player["Age"]},
                    {player["Height"]},
                    {player["Weight"]},
                    {player["Overall"]},
                    {player["Pace"]},
                    {player["Shooting"]},
                    {player["Passing"]},
                    {player["Dribbling"]},
                    {player["Defending"]},
                    {player["Physicality"]},
                    "{player["Att work rate"]}",
                    "{player["Def work rate"]}",
                    "{player["Preferred foot"]}",
                    {player["Weak foot"]},
                    {player["Skill moves"]},
                    "{player["Gender"]}"
                )
                """
            )

            try:
                self.__cur.execute(query)
            except:
                print(f"{player['Name']} failed to insert.")

        self.__conn.commit()

    def display_table(self, table: str, limit: int = 5) -> None:
        self.__cur.execute(f"""SELECT * FROM {table} LIMIT {limit}""")
        for row in self.__cur.fetchall():
            print(row)

    def display_owned_players(self, UserID: int) -> None:
        self.__cur.execute(
            f"""
            SELECT Name, Position, Overall
            FROM Player
            WHERE PlayerID IN (
                SELECT DISTINCT PlayerID
                FROM Collection
                WHERE Collection.UserID = {UserID}
            )
            ORDER BY Overall DESC;
            """
        )

        for row in self.__cur.fetchall():
            print(row)

    def truncate_table(self, table: str) -> None:
        self.__cur.execute(f"DELETE FROM {table};")
        self.__conn.commit()

if __name__ == '__main__':
    db = Database()
    db.display_table("Player")