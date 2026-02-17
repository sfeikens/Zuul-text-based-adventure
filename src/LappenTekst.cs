class Lore
{
    public string GetLore(string caseType)
    {
        switch (caseType)
        {
            case "MainLore":
                return @"You are a broke ahh college student. You need to use your schools resources to make it.
Your objectives vary from taking a shower, finding food, getting some exercise in and more(not yet added).
Good luck!";
            case "outside":
                return @"You are outside the college, in the main yard. It is dark and you are not allowed to be here right now.
You had to climb over the pub to get in here, soooo.... don't get caught?";
            case "theatre":
                return @"The theatre! Along with the library, it is the oldest building on these grounds.
Built in 1699, and restored in in the 50's after the war, it has no competitor in the age department. And it does show!
The cracks in the limestone and marble flooring, the faded colors of the now ancient paint from the original parts of the building
and an original bookcase up for the display. Better to tread this place carefully...";
            case "pub":
                return @"The old pub. Not much to say, is there? The story displays itself. The old pictures of previous college soccer teams,
pictures of the founder of this collage, old and faded posters of previous parties and events hosted here.
Better to stay quiet though, the owner sleeps upstairs!";
            case "gym":
                return @"The new gym. It was built a year before you came here. There's nothing special about it.
Showers are on the second floor! The stairs are closed due to a slipping hazard, so you'll have to find another way up.";
            case "library":
                return @"The old library. It is ancient. Built in 1699, it is the oldest building together with the theatre.
One of it's bookcases is displayed in the theatre for visitors to see.
The stairs op to the first floor is still an original but are not used anymore.
Maybe the books up there contain some more information about the colleges history?(to be added)";
            case "lab":
                return @"The college lab. Not much to say besides don't eat that apple!";
            case "gymupper":
                return @"Literally just the gym showers.";
            case "office":
                return @"The office. Boring ol' office. Located south of the library, it is still the same old office built a century ago.
It smells like it too. So much dust has accumalated on top of the door that if you shut it too hard, you get showered by dust particles.";
            case "forge":
                return @"A... forge? In a college?. The library should contain more information about it, but I never bothered looking.
All I know is that it is still in operation and has some basic molds available for everybody to use, for whatever reason.";
            case "sword":
                return "Be carefull, it may not be as sharp anymore but it's still dangerous.";
            case "enhanced_sword":
                return "What do you plan on doing with that?";
            case "sus_apple":
                return "Don't eat that!!";
            case "key":
                return "A skeleton key. EXTREMELY fragile! Single use only!";
            case "map":
                return "A map of the college, with some of my own notes about important items around the place.";
            case "epstein_island":
                return @"www.justice.gov/epstein, jmail.world and adjacent";
            case null:
                return "Get lore about what?";
            default:
                return "There is no lore file about that subject.";
        }
    }
}