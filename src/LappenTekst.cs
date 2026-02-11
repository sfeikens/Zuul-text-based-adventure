class Lore
{
    public string GetLore(string caseType)
    {
        switch (caseType)
        {
            case "MainLore":
                return @"You are a broke ahh college student. You need to use your schools resources to make it.
Your objectives vary from taking a shower, finding food, getting some exercise in and more.
Good luck brother.";
            case "outside":
                return @"You are outside the college, in the main yard. It is dark and you are not allowed to be here right now.
You had to climb over the pub to get in here, so don't get caught!";
            case "theatre":
                return @"Ahhh, the theatre! Along with the library, it is the oldest building on these grounds.
Built in 1699, and restored in in the 50's after the war, it has no competitor in the age department. And it does show!
The cracks in the limestone and marble flooring, the faded colors of the now ancient paint from the original parts of the building
and an original bookcase up for the display. Marvelous, isn't it?";
            case "pub":
                return @"The old pub. Not much to say, is there? The story displays itself. The old pictures of previous college soccer teams,
pictures of the founder of this collage, old and faded posters of previous parties and events hosted here.
Better to stay quiet though, the owner sleeps upstairs!";
            case "gym":
                return @"The new gym. It was built a year before you came here. There's nothing special about it.
Showers are on the second floor! The stairs are closed so you'll have to find another way up";
            default:
                return "";
        }
    }
}