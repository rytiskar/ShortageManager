## Usage

### Register a New Shortage

`shortageManager register <User> <Title> <Name> <Room> <Category> <Priority>`

- `<User>`: Creator of the shortage.
- `<Title>`: Title of the shortage.
- `<Name>`: Name of the shortage.
- `<Room>`: Room where the shortage is located (MeetingRoom / kitchen / bathroom).
- `<Category>`: Category of the shortage (Electronics / Food / Other).
- `<Priority>`: Priority of the shortage (1 - not important, 10 - very important).

### List Shortages with Filtering Options

`shortageManager list <User> [-t <Title>] [-s <CreatedOnStart>] [-e <CreatedOnEnd>] [-c <Category>] [-r <Room>]`

- `<User>`: Creator of the shortage.
- `-t, --title`: Filter shortages by title.
- `-s, --createdonstart`: Filter shortages created on or after this date.
- `-e, --createdonend`: Filter shortages created on or before this date.
- `-c, --category`: Filter shortages by category.
- `-r, --room`: Filter shortages by room.

### Delete a Shortage

`shortageManager delete <User> <Title> <Room>`

- `<User>`: Creator of the shortage.
- `<Title>`: Title of the shortage.
- `<Room>`: Room where the shortage is located (MeetingRoom / kitchen / bathroom).

## Getting Started

1. Clone the repository.
2. Build the solution.
3. Run the `shortageManager` executable with the desired command.
