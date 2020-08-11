import os
import shutil
import zipfile
from sys import exit
from typing import List


def create_outs():
    if not os.path.isfile("./DiscordRichPresence/bin/Debug/DiscordRichPresence.dll"):
        exit("Output dll does not exist. Did you build the project?")

    os.makedirs("out/x86/GameData/DiscordRichPresence/Plugins")
    os.makedirs("out/x86_64/GameData/DiscordRichPresence/Plugins")

    shutil.copyfile(
        "./DiscordRichPresence/bin/Debug/DiscordRichPresence.dll",
        "out/x86/GameData/DiscordRichPresence/Plugins/DiscordRichPresence.dll",
    )
    shutil.copyfile(
        "./DiscordRichPresence/bin/Debug/DiscordRichPresence.dll",
        "out/x86_64/GameData/DiscordRichPresence/Plugins/DiscordRichPresence.dll",
    )

    shutil.copyfile(
        "./lib/x86/discord_game_sdk.dll",
        "./out/x86/GameData/DiscordRichPresence/Plugins/discord_game_sdk.dll",
    )
    shutil.copyfile(
        "./lib/x86_64/discord_game_sdk.dll",
        "./out/x86_64/GameData/DiscordRichPresence/Plugins/discord_game_sdk.dll",
    )


def zip_outs():
    def get_files(directory: str) -> List[str]:
        out: List[str] = []
        for root, dirs, files in os.walk(directory):
            for filename in files:
                file = os.path.join(root, filename)
                out.append(file)

        return out

    x86_files = get_files("./out/x86")
    x86_64_files = get_files("./out/x86_64")

    with zipfile.ZipFile("DiscordRichPresence_x86.zip", "w") as z:
        for file in x86_files:
            z.write(file, "./" + file.strip("./out/x86\\"))

    with zipfile.ZipFile("DiscordRichPresence_x86_64.zip", "w") as z:
        for file in x86_64_files:
            z.write(file, "./" + file.strip("./out/x86_64\\"))


if __name__ == "__main__":
    create_outs()
    zip_outs()
