import sqlite3

DATABASE_PATH = "database/players.db"

class Database:
    def __init__(self):
        self.__conn = sqlite3.connect(DATABASE_PATH)
        self.__cur = self.__conn.cursor()

    def create_tables(self) -> None:
        self.__cur.execute(
            """
            CREATE TABLE IF NOT EXISTS Country (
                Name TEXT PRIMARY KEY,
                Flag TEXT
            );
            """
        )

        self.__cur.execute(
            """
            CREATE TABLE IF NOT EXISTS Club (
                ID INTEGER PRIMARY KEY,
                Name TEXT NOT NULL,
                Logo TEXT,
                Colour TEXT
            );
            """
        )

        self.__cur.execute(
            """
            CREATE TABLE IF NOT EXISTS Player (
                ID INTEGER PRIMARY KEY,
                ClubID INTEGER NOT NULL,
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
                Gender CHAR NOT NULL,
                FOREIGN KEY (Nation) REFERENCES Country(Name),
                FOREIGN KEY (ClubID) REFERENCES Club(ID)
            );
            """
        )

        self.__cur.execute(
            """
            CREATE TABLE IF NOT EXISTS User (
                ID INTEGER PRIMARY KEY,
                Username TEXT NOT NULL,
                Password TEXT NOT NULL
            );
            """
        )

        self.__cur.execute(
            """
            CREATE TABLE IF NOT EXISTS Collection (
                UserID INTEGER NOT NULL,
                PlayerID INTEGER NOT NULL,
                Date TEXT NOT NULL,
                FOREIGN KEY (UserID) REFERENCES User(ID),
                FOREIGN KEY (PlayerID) REFERENCES Player(ID)
            );
            """
        )

        self.__conn.commit()

    def populate_players(self, players: list) -> None:
        for player in players:
            _, country_flag = self.get_id("Country", "Name", player["Nation"], "Name")
            club_id, club_flag = self.get_id("Club", "Name", player["Club"], "ID")
            if not country_flag:
                self.__cur.execute(
                    f"""
                    INSERT INTO Country (Name, Flag)
                    VALUES ("{player["Nation"]}", "NotImplemented");
                    """
                )

            if not club_flag:
                self.__cur.execute(
                    f"""
                    INSERT INTO Club (ID, Name)
                    VALUES ({club_id}, "{player["Club"]}");
                    """
                )
            
            # Insert the player
            query = (
                f"""
                INSERT INTO Player (
                    ID,
                    ClubID,
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
                    "{player['Nation']}",
                    {club_id},
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

    def get_id(self, table: str, field: str, value: str, identifier) -> (int, bool):
        """
        Returns the ID of a value in a field in a table.

        If the value doesn't exist already, a new ID is generated.
        Note a new record is NOT created.
        """
        self.__cur.execute(f"""SELECT {identifier} FROM {table} WHERE {field} = "{value}" """)
        existing_id = self.__cur.fetchone()

        if existing_id:
            return (existing_id[0], True)

        self.__cur.execute(f"SELECT {identifier} FROM {table}")
        existing_ids = set(row[0] for row in self.__cur.fetchall())

        unused_id = 0
        while unused_id in existing_ids:
            unused_id += 1
        return (unused_id, False)

    def display_table(self, table: str, limit: int = 5) -> None:
        self.__cur.execute(f"""SELECT * FROM {table} LIMIT {limit}""")

        for row in self.__cur.fetchall():
            print(row)
    
    def get_players(self, num_of_players, min_rating=75, max_rating=99):
        self.__cur.execute(
            f"""
            SELECT PlayerID, Name, Position, Overall
            FROM Player 
            WHERE Overall >= {min_rating}
            AND Overall <= {max_rating}
            ORDER BY RANDOM()
            LIMIT {num_of_players};
            """
        )

        for row in self.__cur.fetchall():
            print(row[1:])

    def add_player_to_collection(self, UserID: int, PlayerID: int) -> None:
        self.__cur.execute(
            f"""
            INSERT INTO Collection (UserID, PlayerID, Date)
            VALUES ({UserID}, {PlayerID}, DATE('now'));
            """
        )

        self.__conn.commit()


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
    db.display_table("Club")