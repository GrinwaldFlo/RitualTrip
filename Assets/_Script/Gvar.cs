using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

internal static class Gvar
{
	internal const int nbPlayerMax = 8;
	internal static enLanguage lng = enLanguage.English;

	internal static enGameState gameState { get; private set; }
	internal static List<clHero> lstHeros;
	internal static List<clPlace> lstPlace;
	internal static List<clEmotion> lstEmotion;
	internal static List<clAction> lstAction;
	internal static List<clObject> lstObject;
	internal static List<clResult> lstResult;

	internal static string[] emotionStr = new string[] { "depressive" , "sadistic" , "lubric" , "despotic" , "ecstatic" , "peaceful" ,
		"puritain" , "idealist" };

	internal static string[] actionStr = new string[] { "Help", "Block", "Do nothing"};

	internal static void setGameState(enGameState state)
	{
		gameState = state;
	}

	internal static string addDet(string val)
	{
		if(lng == enLanguage.English)
		{
			if(val.StartsWith("uni"))
				return "a " + val;

			switch (val[0])
			{
				case 'a':
				case 'e':
				case 'i':
				case 'o':
				case 'u':
				case 'y':
					return "an " + val;
				default:
					return "a " + val;
			}
		}
		return "YY" + val;
	}

	internal static void init()
	{
		lstHeros = new List<clHero>();
		lstEmotion = new List<clEmotion>();
		lstPlace = new List<clPlace>();
		lstAction = new List<clAction>();
		lstObject = new List<clObject>();
		lstResult = new List<clResult>();

		lstPlace.Add(new clPlace("a good old dungeon", "donjon", "You just arrived in a closed, dark and humid place... you can hear people screaming... some of them are far away, some of them are not. You can feel a massive building above you. You are in a good old dungeon full of stones, pain and delusions.", ""));
		lstPlace.Add(new clPlace("an intergalactic station", "base sidérale", "In your guts you know you are not on Earth anymore. But...this is not a moon, this is a space station ! Your presence is forbidden, you know you shouldn't stay here for long...", ""));
		lstPlace.Add(new clPlace("an enchanted forest", "forêt enchantée", "Sounds of birds, wind in the leaves... and feminin laughts everywhere... maybe in your minds... this forest is not an ordinary one. Something in the woods is looking at you... something in the woods is waiting for you.", ""));

		lstPlace.Add(new clPlace("a supermarket", "supermarché", "Music, sacred elements, this is a modern temple : everyone has to go there, the meaning of life is there. Grab a shopping cart and do your duty.", ""));
		lstPlace.Add(new clPlace("a train", "train", "You are in the belly of an iron horse. Travelers are hoping to get to a destination, to meet new strange friends... like you !", ""));



		lstObject.Add(new clObject("deadBody", "a rotting dead body", "cadavre"));
		lstObject.Add(new clObject("altar", "a mysterious altar", "autel"));
		lstObject.Add(new clObject("rubberChicken", "a rubber chicken", "poulet en caoutchouc"));
		lstObject.Add(new clObject("lightSaber", "a regular light saber", "sabre laser"));
		lstObject.Add(new clObject("chickenSandwich", "a chicken sandwich", "sandwich au poulet"));
		lstObject.Add(new clObject("mobilePhone", "a mobile phone", "natel"));
		lstObject.Add(new clObject("flower", "a flower", "fleur"));
		lstObject.Add(new clObject("hamster", "a hamster", "hamster"));
		lstObject.Add(new clObject("chandelier", "a chandelier", "chandelier"));



		lstAction.Add(new clAction("depressive", "is comiting suicide with {0}", "It is a respectfull choice, let's make it quicker, it is obvious that this person doesn't know how to do it well.", "Let's save this life ! No one could be let alone in this moment of mind loss.", "Nothing to win, let's do nothing.", "Quicker is better, you a let another dead body behind you and slide in another world.", "Another life saved, what can bring more satisfaction ? Let's hope the next world will be less desesperate.", "People dies... that's what people do."));
		lstAction.Add(new clAction("sadistic", "is torturing {0}", "Let's have fun, join the party ! Which part is still untouched ?", "No way... this must come to an end right now !", "Not your business, let's go to another world.", "This was soooo fun, you can't wait for another world, full of such nice moments...", "Damn lunatics... Hoping that one day the victim will be able to recover, you get through the Ritual portal with a deep sens of accomplishment.", "Well... pleasure must be taken where it can be taken."));
		lstAction.Add(new clAction("lubric", "is having pleasure with {0}", "Fun can always be shared. You never know when it will be the last time...", "Let's stop this, it is totally insane. ", "Weird... but we don't care.", "Exhausted and smily, you succeed to finally decide to keep on the Ritual.", "Some acts shouldn't be accomplished... Just BECAUSE ! Now jump to another universe, hopefully less disgusting.", "If something exists in this world... it surely already has been used this way by a human. Nothing new."));
		lstAction.Add(new clAction("despotic", "is rudely gibing orders to {0}", "Better to be on the ruler's side of social life... You can even enjoy it !", "This is unfair, let's stop this.", "Some of them wants to use you, some of them wants to get used by you... nothing new, let's get out of here.", "Such a nice feeling to be on the bright side of the authorities. Proud of yourself you enter in a new world. ", "No one should endure such a pressure... NO PASARAN ! Time to jump.", "Being involved in politics ? WTF, we have a Ritual to accomplish..."));
		lstAction.Add(new clAction("ecstatic", "can't stop talking about {0}", "This is fantastic, we must say something about it, let's write a poem.", "This is stupid... we can't let this happening", "WHO CARES ?", "Universe is a perfect place, not a day should be wasted by not saying it. Time to open a new cool page of your story now...", "Nonsens is nonsens. (Hit the singer till it STFU) Universe is more coherent now. Ready to jump.", "Reality is not dark or nice. It is. Nothing to enjoy or to be depleased of."));
		lstAction.Add(new clAction("peaceful", "is dancing with {0}", "Peace on earth, spread the message... We may be dreamers, but we are not the only ones.", "Fucking hippies... open your eyes to the real world and get a job !", "... really ? NEXT !!!", "What a bliss... cheers and join the dance !", "Silliness is an insult to reality. Let's make all the useless and disturbing dancers to stop that !", "We don't dance ok ? We don't hear anything... let's go."));
		lstAction.Add(new clAction("puritan", "is lecturing {0}", "The right way must be explained and pointed right now to the fools ! This is our duty as righteous people !", "So many stupid words... this must be ended by all means.", "Nonsens... next world.", "No one can negate your deep sens morality now. Time to spread the good word in another space-time.", "No one should decide and impose his so called \"right way\". If truth exists it doesn't need defenders.", "The strong rules the weak... eternal story, boring..."));
		lstAction.Add(new clAction("idealist", "is leading a (solo) riot in the name of {0}", "Get up ! Stand up for your rights ! Freedom is just ahead !", "Damn anarchists hurting your ears... let's defend order against those silly idealists ! You shall not pass !", "This is not our war.", "Freedom is a fight, it has a price. Let's run to another chance to defend it ! ", "Society needs order, some of us needs to do the bad job to protect the stability.", "No one cares... let's not get involved in that."));



		lstEmotion.Add(new clEmotion(0, "depressive", "dépressif", "Your are not in a good mood... flowers, little animals and positive attitude helps you to feel better. ", "Mankind has decided to come to an end in order to let the opportunity to another form of intelligence to appear. You will leave and participate to this final action."));
		lstEmotion.Add(new clEmotion(1, "sadistic", "sadique", "Seeing any lifeform suffering pleases you.... a lot.", "In this world, painfull actions are allowed after 20H00, torture is a fancy art."));
		lstEmotion.Add(new clEmotion(2, "lubric", "lubrique", "Well... you are a bit focus on ... you know... and any kinds of objects that makes you think about it.", "All the economy, the political system and sport activity are ... lustful based."));
		lstEmotion.Add(new clEmotion(3, "despotic", "autoritaire", "You really enjoy when people do what you want... or can't do what they want.", "You are the eternal dictator of a world submitted to your will. From time to time, you intigate a rebel mouvement in order to crush it and to enjoy your almighty power."));
		lstEmotion.Add(new clEmotion(4, "ecstatic", "extatique", "You are always content. Positiv actions please you. You don't like negative attitude.", "Everything is a source of joy, every action or object is new and full of interesting sensations (maybe air is satured with natural serotonine)"));
		lstEmotion.Add(new clEmotion(5, "peaceful", "pacifique", "Peace on earth and everywhere else. You really dont like agressions.", "Kisses and hugs became the currency. War is a poetry contest."));
		lstEmotion.Add(new clEmotion(6, "puritan", "puritain", "You are a bit extreme... anything dealing with rituals (or dead bodies) is sacred for you.", "In this world pulsions that are against the moral order is controled and punished. Pure people are finally rewarded and acceed the the social level they deserve."));
		lstEmotion.Add(new clEmotion(7, "idealist", "libertaire", "Autorithy is your enemy, you are always ready to rise against it.", "Woodstock never got to an end. From USA to the world, hippies won. Peace, love and liberty everywhere."));

		lstHeros.Add(new clHero("paladin", "paladin", "a fatty paladin, in a shiny and fancy armor with fine pink decorations", "an ordinary fanatic paladin, in a glowing magical armor"));
		lstHeros.Add(new clHero("plumber", "plombier", "a polite Polish plumber, with a Romeo and Juliet tattoo on his arm", "a japanese plumber with an italian accent, with a great mustache and a red cap"));
		lstHeros.Add(new clHero("trekker", "trekker", "an old and noble trekker, with the cleanest blue shirt you ever saw", "an young nearsighted trekkie with a dirty blue shirt full of strange smudges"));
		lstHeros.Add(new clHero("accountant", "comptable", "a standard accountant, with a black tie and a black briefcase", "a maniac accountant, holding a mysterious black breifcase and a scary black tia"));
		lstHeros.Add(new clHero("murderer", "assassin", "a quiet guy hidden in a white robe", "a perfect weapon of mass destruction wearing a white hood"));
		lstHeros.Add(new clHero("screenwriter", "scénariste", "a crazy screenwriter holding a pen", "a lazy tv show screenwriter"));
		lstHeros.Add(new clHero("nurse", "infirmière", "the sexiest nurse in a very short uniform", "an old, hairy and smelly nurse"));
		lstHeros.Add(new clHero("necromancer", "nécromancien", "a young and unskilled necromancer", "a hurt and bleeding necromancer"));
		lstHeros.Add(new clHero("soldier", "soldat", "a scary soldier, with a durty uniform", "a three stars drunk general with an open uniform"));
		lstHeros.Add(new clHero("unicorn", "licorne", "Charlie the unicorn himself", "the last unicorn in all universes, with stars in its eyes and goldy clogs"));



		lstResult.Add(new clResult(0, "depressive", "deadBody", 1, 1, 0, 0, -1, -1, -1, 1));
		lstResult.Add(new clResult(0, "depressive", "altar", 1, 0, 0, 0, -1, -1, -1, 1));
		lstResult.Add(new clResult(0, "depressive", "rubberChicken", 1, 0, 0, 0, -1, -1, -1, 1));
		lstResult.Add(new clResult(0, "depressive", "lightSaber", 1, 0, 0, 0, -1, -1, -1, 1));
		lstResult.Add(new clResult(0, "depressive", "chickenSandwich", 1, 0, 0, 0, -1, -1, -1, 1));
		lstResult.Add(new clResult(0, "depressive", "mobilePhone", 1, 0, 0, 0, -1, -1, -1, 1));
		lstResult.Add(new clResult(0, "depressive", "flower", 1, 1, 0, 0, -1, -1, -1, 1));
		lstResult.Add(new clResult(0, "depressive", "hamster", 1, 1, 0, 0, -1, -1, -1, 1));
		lstResult.Add(new clResult(0, "depressive", "chandelier", 1, 0, 0, 0, -1, -1, -1, 1));
		lstResult.Add(new clResult(0, "sadistic", "deadBody", 0, 1, 1, 1, -1, -1, -1, -1));
		lstResult.Add(new clResult(0, "sadistic", "altar", 0, 1, 0, 1, -1, -1, -1, -1));
		lstResult.Add(new clResult(0, "sadistic", "rubberChicken", 0, 1, 0, 1, -1, -1, -1, -1));
		lstResult.Add(new clResult(0, "sadistic", "lightSaber", 0, 1, 0, 1, -1, -1, -1, -1));
		lstResult.Add(new clResult(0, "sadistic", "chickenSandwich", 0, 1, 0, 1, -1, -1, -1, -1));
		lstResult.Add(new clResult(0, "sadistic", "mobilePhone", 0, 1, 0, 1, -1, -1, -1, -1));
		lstResult.Add(new clResult(0, "sadistic", "flower", 0, 1, 0, 1, -1, -1, -1, -1));
		lstResult.Add(new clResult(0, "sadistic", "hamster", 0, 1, 1, 1, -1, -1, -1, -1));
		lstResult.Add(new clResult(0, "sadistic", "chandelier", 0, 1, 0, 1, -1, -1, -1, -1));
		lstResult.Add(new clResult(0, "lubric", "deadBody", 0, 1, 1, 0, -1, -1, -1, 0));
		lstResult.Add(new clResult(0, "lubric", "altar", 0, 1, 1, 0, -1, 0, -1, 0));
		lstResult.Add(new clResult(0, "lubric", "rubberChicken", 0, 1, 1, 0, -1, 0, -1, 0));
		lstResult.Add(new clResult(0, "lubric", "lightSaber", 0, 1, 1, 0, -1, 0, -1, 0));
		lstResult.Add(new clResult(0, "lubric", "chickenSandwich", 0, 1, 1, 0, -1, 0, -1, 0));
		lstResult.Add(new clResult(0, "lubric", "mobilePhone", 0, 1, 1, 0, -1, 0, -1, 0));
		lstResult.Add(new clResult(0, "lubric", "flower", 0, 1, 1, 0, -1, -1, -1, 0));
		lstResult.Add(new clResult(0, "lubric", "hamster", 0, 1, 1, 0, -1, -1, -1, 0));
		lstResult.Add(new clResult(0, "lubric", "chandelier", 0, 1, 1, 0, -1, 0, -1, 0));
		lstResult.Add(new clResult(0, "despotic", "deadBody", 0, 1, 0, 1, -1, -1, 1, 1));
		lstResult.Add(new clResult(0, "despotic", "altar", 0, 1, 0, 1, -1, -1, 1, 1));
		lstResult.Add(new clResult(0, "despotic", "rubberChicken", 0, 1, 0, 1, -1, -1, 1, 1));
		lstResult.Add(new clResult(0, "despotic", "lightSaber", 0, 1, 0, 1, -1, -1, 1, 1));
		lstResult.Add(new clResult(0, "despotic", "chickenSandwich", 0, 1, 0, 1, -1, -1, 1, 1));
		lstResult.Add(new clResult(0, "despotic", "mobilePhone", 0, 1, 0, 1, -1, -1, 1, 1));
		lstResult.Add(new clResult(0, "despotic", "flower", 0, 1, 0, 1, -1, -1, 1, 1));
		lstResult.Add(new clResult(0, "despotic", "hamster", 0, 1, 0, 1, -1, -1, 1, 1));
		lstResult.Add(new clResult(0, "despotic", "chandelier", 0, 1, 0, 1, -1, -1, 1, 1));
		lstResult.Add(new clResult(0, "ecstatic", "deadBody", 0, -1, 0, -1, 1, 1, -1, 1));
		lstResult.Add(new clResult(0, "ecstatic", "altar", 0, -1, 0, -1, 1, 1, -1, 1));
		lstResult.Add(new clResult(0, "ecstatic", "rubberChicken", 0, -1, 0, -1, 1, 1, -1, 1));
		lstResult.Add(new clResult(0, "ecstatic", "lightSaber", 0, -1, 0, -1, 1, 1, -1, 1));
		lstResult.Add(new clResult(0, "ecstatic", "chickenSandwich", 0, -1, 0, -1, 1, 1, -1, 1));
		lstResult.Add(new clResult(0, "ecstatic", "mobilePhone", 0, -1, 0, -1, 1, 1, -1, 1));
		lstResult.Add(new clResult(0, "ecstatic", "flower", 0, -1, 0, -1, 1, 1, -1, 1));
		lstResult.Add(new clResult(0, "ecstatic", "hamster", 0, -1, 0, -1, 1, 1, -1, 1));
		lstResult.Add(new clResult(0, "ecstatic", "chandelier", 0, -1, 0, -1, 1, 1, -1, 1));
		lstResult.Add(new clResult(0, "peaceful", "deadBody", 1, -1, 0, 0, 1, 1, 1, -1));
		lstResult.Add(new clResult(0, "peaceful", "altar", 0, -1, 0, 0, 1, 1, 1, -1));
		lstResult.Add(new clResult(0, "peaceful", "rubberChicken", 0, -1, 0, 0, 1, 1, 1, -1));
		lstResult.Add(new clResult(0, "peaceful", "lightSaber", 0, -1, 0, 0, 1, 1, 1, -1));
		lstResult.Add(new clResult(0, "peaceful", "chickenSandwich", 0, -1, 0, 0, 1, 1, 1, -1));
		lstResult.Add(new clResult(0, "peaceful", "mobilePhone", 0, -1, 0, 0, 1, 1, 1, -1));
		lstResult.Add(new clResult(0, "peaceful", "flower", 0, -1, 0, 0, 1, 1, 1, -1));
		lstResult.Add(new clResult(0, "peaceful", "hamster", 0, -1, 0, 0, 1, 1, 1, -1));
		lstResult.Add(new clResult(0, "peaceful", "chandelier", 0, -1, 0, 0, 1, 1, 1, -1));
		lstResult.Add(new clResult(0, "puritan", "deadBody", 0, 1, 0, 1, 0, -1, 1, -1));
		lstResult.Add(new clResult(0, "puritan", "altar", 0, 1, 0, 1, 0, -1, 1, -1));
		lstResult.Add(new clResult(0, "puritan", "rubberChicken", 0, 1, 0, 1, 0, -1, 1, -1));
		lstResult.Add(new clResult(0, "puritan", "lightSaber", 0, 1, 0, 1, 0, -1, 1, -1));
		lstResult.Add(new clResult(0, "puritan", "chickenSandwich", 0, 1, 0, 1, 0, -1, 1, -1));
		lstResult.Add(new clResult(0, "puritan", "mobilePhone", 0, 1, 0, 1, 0, -1, 1, -1));
		lstResult.Add(new clResult(0, "puritan", "flower", 0, 1, 0, 1, 0, -1, 1, -1));
		lstResult.Add(new clResult(0, "puritan", "hamster", 0, 1, 0, 1, 0, -1, 1, -1));
		lstResult.Add(new clResult(0, "puritan", "chandelier", 0, 1, 0, 1, 0, -1, 1, -1));
		lstResult.Add(new clResult(0, "idealist", "deadBody", 0, 0, 0, 1, 0, -1, 1, 1));
		lstResult.Add(new clResult(0, "idealist", "altar", 0, 0, 0, 1, 0, -1, 1, 1));
		lstResult.Add(new clResult(0, "idealist", "rubberChicken", 0, 0, 0, 1, 0, -1, 1, 1));
		lstResult.Add(new clResult(0, "idealist", "lightSaber", 0, 0, 0, 1, 0, -1, 1, 1));
		lstResult.Add(new clResult(0, "idealist", "chickenSandwich", 0, 0, 0, 1, 0, -1, 1, 1));
		lstResult.Add(new clResult(0, "idealist", "mobilePhone", 0, 0, 0, 1, 0, -1, 1, 1));
		lstResult.Add(new clResult(0, "idealist", "flower", 0, 0, 0, 1, 0, -1, 1, 1));
		lstResult.Add(new clResult(0, "idealist", "hamster", 0, 0, 0, 1, 0, -1, 1, 1));
		lstResult.Add(new clResult(0, "idealist", "chandelier", 0, 0, 0, 1, 0, -1, 1, 1));



		lstResult.Add(new clResult(1, "depressive", "deadBody", -1, -1, 0, 0, 1, 1, 1, 1));
		lstResult.Add(new clResult(1, "depressive", "altar", -1, 0, 0, 0, 1, 1, 2, 1));
		lstResult.Add(new clResult(1, "depressive", "rubberChicken", -1, 0, 0, 0, 1, 1, 1, 1));
		lstResult.Add(new clResult(1, "depressive", "lightSaber", -1, 0, 0, 0, 1, 1, 1, 1));
		lstResult.Add(new clResult(1, "depressive", "chickenSandwich", -1, 0, 0, 0, 1, 1, 1, 1));
		lstResult.Add(new clResult(1, "depressive", "mobilePhone", -1, 0, 0, 0, 1, 1, 1, 0));
		lstResult.Add(new clResult(1, "depressive", "flower", -1, 0, 0, 0, 2, 2, 1, 1));
		lstResult.Add(new clResult(1, "depressive", "hamster", -1, -2, -1, 0, 2, 2, 0, 1));
		lstResult.Add(new clResult(1, "depressive", "chandelier", -1, -2, 0, 0, 1, 1, 1, 1));
		lstResult.Add(new clResult(1, "sadistic", "deadBody", 0, -2, 0, 0, 1, 1, 2, 1));
		lstResult.Add(new clResult(1, "sadistic", "altar", 0, 1, 0, 0, 1, 0, 2, -1));
		lstResult.Add(new clResult(1, "sadistic", "rubberChicken", 0, -1, 1, 0, 1, 1, 1, 1));
		lstResult.Add(new clResult(1, "sadistic", "lightSaber", 0, -1, 1, 0, 1, 1, 0, 1));
		lstResult.Add(new clResult(1, "sadistic", "chickenSandwich", 0, -1, 0, 0, 0, 0, 0, 0));
		lstResult.Add(new clResult(1, "sadistic", "mobilePhone", 0, -1, 0, 0, 0, 0, 0, -1));
		lstResult.Add(new clResult(1, "sadistic", "flower", 0, -1, 0, 0, 2, 1, 0, 1));
		lstResult.Add(new clResult(1, "sadistic", "hamster", 0, -2, 0, 0, 2, 2, 0, 1));
		lstResult.Add(new clResult(1, "sadistic", "chandelier", 0, 0, 0, 0, 0, 0, 2, -1));
		lstResult.Add(new clResult(1, "lubric", "deadBody", 0, -1, -2, 1, 2, 1, 2, 1));
		lstResult.Add(new clResult(1, "lubric", "altar", 0, 0, -2, 1, -1, 0, 2, -1));
		lstResult.Add(new clResult(1, "lubric", "rubberChicken", 0, 1, -2, 1, -1, 0, 1, -1));
		lstResult.Add(new clResult(1, "lubric", "lightSaber", 0, 1, -2, 1, 1, 1, 1, 1));
		lstResult.Add(new clResult(1, "lubric", "chickenSandwich", 0, 1, -2, 1, 1, 1, 1, 1));
		lstResult.Add(new clResult(1, "lubric", "mobilePhone", 0, 1, -2, 1, 0, 0, 1, 1));
		lstResult.Add(new clResult(1, "lubric", "flower", 0, 1, -2, 1, 1, 1, 1, 0));
		lstResult.Add(new clResult(1, "lubric", "hamster", 0, 1, -2, 1, 2, 1, 1, 1));
		lstResult.Add(new clResult(1, "lubric", "chandelier", 0, 1, -2, 1, 0, 0, 2, 0));
		lstResult.Add(new clResult(1, "despotic", "deadBody", 0, 0, 0, -1, 1, 1, 1, 2));
		lstResult.Add(new clResult(1, "despotic", "altar", 0, 0, 0, -1, 0, 1, 2, 0));
		lstResult.Add(new clResult(1, "despotic", "rubberChicken", 0, 0, 0, -1, 0, 0, 0, 0));
		lstResult.Add(new clResult(1, "despotic", "lightSaber", 0, 0, 0, -1, 0, 0, 0, 0));
		lstResult.Add(new clResult(1, "despotic", "chickenSandwich", 0, 0, 0, -1, 0, 0, 0, 0));
		lstResult.Add(new clResult(1, "despotic", "mobilePhone", 0, 0, 0, -1, 0, 0, 0, 0));
		lstResult.Add(new clResult(1, "despotic", "flower", 0, 0, 0, -1, 1, 1, 0, 1));
		lstResult.Add(new clResult(1, "despotic", "hamster", 0, -1, 0, -1, 1, 1, 0, 0));
		lstResult.Add(new clResult(1, "despotic", "chandelier", 0, 0, 0, -1, 0, 0, 1, 0));
		lstResult.Add(new clResult(1, "ecstatic", "deadBody", -1, 0, 0, 1, -1, -1, 2, 0));
		lstResult.Add(new clResult(1, "ecstatic", "altar", -1, 0, 0, 1, -1, -1, -2, 0));
		lstResult.Add(new clResult(1, "ecstatic", "rubberChicken", -1, 0, 0, 1, -1, -1, 0, 0));
		lstResult.Add(new clResult(1, "ecstatic", "lightSaber", -1, 0, 0, 1, -1, -1, 0, 0));
		lstResult.Add(new clResult(1, "ecstatic", "chickenSandwich", -1, 0, 0, 1, -1, -1, 0, 0));
		lstResult.Add(new clResult(1, "ecstatic", "mobilePhone", -1, 0, 0, 1, -1, -1, 0, 1));
		lstResult.Add(new clResult(1, "ecstatic", "flower", -1, 0, 0, 1, -1, -1, 0, 1));
		lstResult.Add(new clResult(1, "ecstatic", "hamster", -1, 0, 0, 1, -1, -1, 0, 1));
		lstResult.Add(new clResult(1, "ecstatic", "chandelier", -1, 0, 0, 1, -1, -1, 0, 1));
		lstResult.Add(new clResult(1, "peaceful", "deadBody", -2, 1, 0, 1, 0, -1, 1, -1));
		lstResult.Add(new clResult(1, "peaceful", "altar", 0, 1, 0, 1, 0, -1, -2, -1));
		lstResult.Add(new clResult(1, "peaceful", "rubberChicken", 0, 1, 0, 1, 0, -1, 0, 0));
		lstResult.Add(new clResult(1, "peaceful", "lightSaber", 0, 1, 0, 1, 0, -1, 0, 0));
		lstResult.Add(new clResult(1, "peaceful", "chickenSandwich", 0, 1, 0, 1, 0, -1, 0, 0));
		lstResult.Add(new clResult(1, "peaceful", "mobilePhone", 0, 1, 0, 1, 0, -1, 0, 1));
		lstResult.Add(new clResult(1, "peaceful", "flower", -1, 1, 0, 1, -1, -1, 0, 0));
		lstResult.Add(new clResult(1, "peaceful", "hamster", -1, 1, 0, 0, -1, -1, 1, 0));
		lstResult.Add(new clResult(1, "peaceful", "chandelier", 0, 1, 0, 0, 1, -1, -2, -1));
		lstResult.Add(new clResult(1, "puritan", "deadBody", 1, 0, 1, 1, 0, 1, -1, 0));
		lstResult.Add(new clResult(1, "puritan", "altar", 0, 0, 0, 1, 0, 1, 0, 0));
		lstResult.Add(new clResult(1, "puritan", "rubberChicken", 0, 0, 0, 1, 0, 1, -1, 1));
		lstResult.Add(new clResult(1, "puritan", "lightSaber", 0, 0, 0, 1, 0, 1, -1, 1));
		lstResult.Add(new clResult(1, "puritan", "chickenSandwich", 0, 0, 0, 1, 0, 1, -1, 1));
		lstResult.Add(new clResult(1, "puritan", "mobilePhone", 0, 0, 0, 1, 0, 0, -1, -1));
		lstResult.Add(new clResult(1, "puritan", "flower", 1, 0, 0, 1, 0, 1, -1, 0));
		lstResult.Add(new clResult(1, "puritan", "hamster", 0, -1, 0, 1, 0, 1, -1, 0));
		lstResult.Add(new clResult(1, "puritan", "chandelier", 0, 1, 0, 1, 0, 1, 0, 0));
		lstResult.Add(new clResult(1, "idealist", "deadBody", 0, 0, 0, 1, 0, 1, 1, -1));
		lstResult.Add(new clResult(1, "idealist", "altar", 0, 0, 0, 1, 0, 1, -1, 0));
		lstResult.Add(new clResult(1, "idealist", "rubberChicken", 0, 0, 0, 1, 0, 1, 0, -1));
		lstResult.Add(new clResult(1, "idealist", "lightSaber", 0, 0, 0, 1, 0, 1, 0, -1));
		lstResult.Add(new clResult(1, "idealist", "chickenSandwich", 0, 0, 0, 1, 0, 1, 0, -1));
		lstResult.Add(new clResult(1, "idealist", "mobilePhone", 0, 0, 0, 1, 0, 1, 0, 0));
		lstResult.Add(new clResult(1, "idealist", "flower", -2, 0, 0, 1, 0, -1, 0, -1));
		lstResult.Add(new clResult(1, "idealist", "hamster", -2, 0, 0, 1, 0, -1, 0, -1));
		lstResult.Add(new clResult(1, "idealist", "chandelier", 0, 0, 0, 1, 0, -1, -1, -1));



		lstResult.Add(new clResult(2, "depressive", "deadBody", 1, 1, 0, -1, -1, -1, -2, -2));
		lstResult.Add(new clResult(2, "depressive", "altar", 1, 1, 0, -1, -1, -1, -3, -1));
		lstResult.Add(new clResult(2, "depressive", "rubberChicken", 1, 1, 0, -1, -1, -1, -1, -1));
		lstResult.Add(new clResult(2, "depressive", "lightSaber", 1, 1, 0, -1, -1, -1, -1, -1));
		lstResult.Add(new clResult(2, "depressive", "chickenSandwich", 1, 1, 0, -1, -1, -1, -1, -1));
		lstResult.Add(new clResult(2, "depressive", "mobilePhone", 1, 1, 0, -1, -1, -1, -1, -1));
		lstResult.Add(new clResult(2, "depressive", "flower", 1, 1, 0, -1, -1, -1, -1, -1));
		lstResult.Add(new clResult(2, "depressive", "hamster", 1, 1, 0, -1, -1, -1, -1, -1));
		lstResult.Add(new clResult(2, "depressive", "chandelier", 1, 0, 0, -1, -1, -1, -2, -1));
		lstResult.Add(new clResult(2, "sadistic", "deadBody", -1, 1, 0, 0, -1, -1, -1, 0));
		lstResult.Add(new clResult(2, "sadistic", "altar", -1, 1, 0, 0, -1, -1, -2, 1));
		lstResult.Add(new clResult(2, "sadistic", "rubberChicken", -1, 1, 0, 0, -1, -1, 0, 0));
		lstResult.Add(new clResult(2, "sadistic", "lightSaber", -1, 1, -1, 0, -1, -1, 0, 0));
		lstResult.Add(new clResult(2, "sadistic", "chickenSandwich", -1, 1, 0, 0, -1, -1, 0, 0));
		lstResult.Add(new clResult(2, "sadistic", "mobilePhone", -1, 1, -1, 0, -1, -1, 0, 1));
		lstResult.Add(new clResult(2, "sadistic", "flower", -1, 1, -1, 0, -1, -1, 0, 0));
		lstResult.Add(new clResult(2, "sadistic", "hamster", -1, 1, 1, 0, -1, -1, 0, 0));
		lstResult.Add(new clResult(2, "sadistic", "chandelier", -1, 1, -1, 0, -1, -1, -1, 0));
		lstResult.Add(new clResult(2, "lubric", "deadBody", 0, 0, 1, -1, 1, 0, -1, 0));
		lstResult.Add(new clResult(2, "lubric", "altar", 0, 0, 1, -1, 1, 0, -3, 0));
		lstResult.Add(new clResult(2, "lubric", "rubberChicken", 0, 0, 1, -1, 1, 0, -1, 0));
		lstResult.Add(new clResult(2, "lubric", "lightSaber", 0, 0, 1, -1, 1, 0, -1, 0));
		lstResult.Add(new clResult(2, "lubric", "chickenSandwich", 0, 0, 1, -1, 1, 0, -1, 0));
		lstResult.Add(new clResult(2, "lubric", "mobilePhone", 0, 0, 1, -1, 1, 0, -1, 0));
		lstResult.Add(new clResult(2, "lubric", "flower", -1, 0, 1, -1, 1, 0, -1, 0));
		lstResult.Add(new clResult(2, "lubric", "hamster", 0, 0, 1, -1, 1, 0, -1, 0));
		lstResult.Add(new clResult(2, "lubric", "chandelier", 0, 0, 1, -1, 1, 0, -2, 0));
		lstResult.Add(new clResult(2, "despotic", "deadBody", -1, 0, 0, 1, 0, -1, 1, -1));
		lstResult.Add(new clResult(2, "despotic", "altar", 0, 0, 0, 1, 0, -1, -2, 0));
		lstResult.Add(new clResult(2, "despotic", "rubberChicken", 0, 0, 0, 1, 0, -1, 1, -1));
		lstResult.Add(new clResult(2, "despotic", "lightSaber", 0, 0, 0, 1, 0, -1, 1, -1));
		lstResult.Add(new clResult(2, "despotic", "chickenSandwich", 0, 0, 0, 1, 0, -1, 1, -1));
		lstResult.Add(new clResult(2, "despotic", "mobilePhone", 0, 0, 0, 1, 0, -1, 1, -1));
		lstResult.Add(new clResult(2, "despotic", "flower", -1, 1, 0, 1, 0, -1, 1, -1));
		lstResult.Add(new clResult(2, "despotic", "hamster", -1, 1, 0, 1, 0, -1, 1, -1));
		lstResult.Add(new clResult(2, "despotic", "chandelier", 0, 0, 0, 1, 0, -1, -1, -1));
		lstResult.Add(new clResult(2, "ecstatic", "deadBody", -1, 0, 0, -1, 1, 1, -2, 0));
		lstResult.Add(new clResult(2, "ecstatic", "altar", 0, 0, 0, -1, 1, 1, 2, -1));
		lstResult.Add(new clResult(2, "ecstatic", "rubberChicken", 0, 0, 0, -1, 1, 1, -1, 0));
		lstResult.Add(new clResult(2, "ecstatic", "lightSaber", 0, 0, 1, -1, 1, 1, -1, 0));
		lstResult.Add(new clResult(2, "ecstatic", "chickenSandwich", 0, 0, 0, -1, 1, 1, -1, 0));
		lstResult.Add(new clResult(2, "ecstatic", "mobilePhone", 0, 0, 1, -1, 1, 1, -1, 0));
		lstResult.Add(new clResult(2, "ecstatic", "flower", 0, 0, 0, -1, 1, 1, -1, 0));
		lstResult.Add(new clResult(2, "ecstatic", "hamster", 0, 0, 0, -1, 1, 1, -1, 0));
		lstResult.Add(new clResult(2, "ecstatic", "chandelier", 0, 0, 1, -1, 1, 1, -1, 0));
		lstResult.Add(new clResult(2, "peaceful", "deadBody", -1, 0, 0, -1, 1, 1, -2, 0));
		lstResult.Add(new clResult(2, "peaceful", "altar", 0, 0, 0, -1, 1, 1, 2, -1));
		lstResult.Add(new clResult(2, "peaceful", "rubberChicken", 0, 0, 0, -1, 1, 1, -1, 0));
		lstResult.Add(new clResult(2, "peaceful", "lightSaber", 0, 0, 1, -1, 1, 1, -1, 0));
		lstResult.Add(new clResult(2, "peaceful", "chickenSandwich", 0, 0, 0, -1, 1, 1, -1, 0));
		lstResult.Add(new clResult(2, "peaceful", "mobilePhone", 0, 0, 1, -1, 1, 1, -1, 0));
		lstResult.Add(new clResult(2, "peaceful", "flower", 0, 0, 0, -1, 1, 1, -1, 0));
		lstResult.Add(new clResult(2, "peaceful", "hamster", 0, 0, 0, -1, 1, 1, -1, 0));
		lstResult.Add(new clResult(2, "peaceful", "chandelier", 0, 0, 1, -1, 1, 1, -1, 0));
		lstResult.Add(new clResult(2, "puritan", "deadBody", -1, 0, 0, 1, 0, -1, 1, -1));
		lstResult.Add(new clResult(2, "puritan", "altar", 0, 0, 0, 1, 0, 0, -2, 0));
		lstResult.Add(new clResult(2, "puritan", "rubberChicken", 0, 1, -1, 1, 0, -1, 1, -1));
		lstResult.Add(new clResult(2, "puritan", "lightSaber", 0, 1, -1, 1, 0, -1, 1, -1));
		lstResult.Add(new clResult(2, "puritan", "chickenSandwich", 0, 1, 0, 1, 0, -1, 1, -1));
		lstResult.Add(new clResult(2, "puritan", "mobilePhone", 0, 1, -1, 1, 0, -1, 1, -1));
		lstResult.Add(new clResult(2, "puritan", "flower", 0, 1, 0, 1, 0, -1, 1, -1));
		lstResult.Add(new clResult(2, "puritan", "hamster", 0, 1, 0, 1, 0, -1, 1, -1));
		lstResult.Add(new clResult(2, "puritan", "chandelier", 0, 1, -1, 1, 0, -1, 1, -1));
		lstResult.Add(new clResult(2, "idealist", "deadBody", 1, 0, 0, -1, 0, -1, -1, 1));
		lstResult.Add(new clResult(2, "idealist", "altar", 0, 0, 0, -1, 0, -1, 2, 1));
		lstResult.Add(new clResult(2, "idealist", "rubberChicken", 0, 0, 1, -1, 0, -1, 0, 1));
		lstResult.Add(new clResult(2, "idealist", "lightSaber", 0, 0, 1, -1, 0, -1, 0, 1));
		lstResult.Add(new clResult(2, "idealist", "chickenSandwich", 0, 0, 0, -1, 0, -1, 0, 1));
		lstResult.Add(new clResult(2, "idealist", "mobilePhone", 0, 0, 1, -1, 0, -1, 0, 1));
		lstResult.Add(new clResult(2, "idealist", "flower", 1, 0, 0, -1, 0, -1, 0, 1));
		lstResult.Add(new clResult(2, "idealist", "hamster", 0, 0, 0, -1, 0, -1, 0, 1));
		lstResult.Add(new clResult(2, "idealist", "chandelier", 0, 0, 1, -1, 0, -1, 0, 1));



		check();
	}

	private static void check()
	{
		foreach (clEmotion item in lstEmotion)
		{
			clAction action = Gvar.lstAction.Find(x => x.emotion == item.getId());
			if (action == null)
			{
				Debug.Log("`!!!!!!!! Emotion not found in action " + item.getId());
			}
		}

		foreach (clResult item in lstResult)
		{
			item.check();
		}
		Debug.Log("--- Check Done ---");
	}
}

internal static class Cmd
{
	internal const string Intro = "#Intro|";
	internal const string Intro2 = "#Intro2|";
	internal const string Play = "#Play|";
	internal const string Result = "#Result|";
	internal const string End = "#End|";
	internal const string HideBut = "#HideBut";
	internal const string But = "#But";
	internal const string Debug = "#Debug|";
	internal const string Descr = "#Descr|";
}

internal static class Lng
{
	internal const string Intro = "Press start to begin";
	internal const string Wait = "Please wait the next game";
}

internal class clObject
{
	internal string id;
	string labelEn;
	string labelFr;

	internal clObject(string id, string labelEn, string labelFr)
	{
		this.id = id;
		this.labelEn = labelEn;
		this.labelFr = labelFr;
	}

	internal object getLabel()
	{
		return labelEn;
	}
}

internal class clAction
{
	internal string emotion;
	internal string sentence;
	internal string help;
	internal string block;
	internal string doNothing;
	internal string resultHelp;
	internal string resultBlock;
	internal string resultDoNothing;

	internal clAction(string emotion, string sentence, string help, string block, string doNothing, string resultHelp, string resultBlock, string resultDoNothing)
	{
		this.emotion = emotion;
		this.sentence = sentence;
		this.help = help;
		this.block = block;
		this.doNothing = doNothing;
		this.resultBlock = resultBlock;
		this.resultDoNothing = resultDoNothing;
		this.resultHelp = resultHelp;
	}

	internal string getTextResult(int id)
	{
		switch (id)
		{
			case 0:
				return resultHelp;
			case 1:
				return resultBlock;
			case 2:
				return resultDoNothing;
			default:
				return "Nothing found...";
		}
	}
}

internal class clHero
{
	string labelEn;
	string labelFr;
	string descrEN1;
	string descrEN2;

	internal clHero(string labelEn, string labelFr, string descrEN1, string descrEN2)
	{
		this.labelEn = labelEn;
		this.labelFr = labelFr;
		this.descrEN1 = descrEN1;
		this.descrEN2 = descrEN2;
	}

	internal string getLabel()
	{
		return labelEn;
	}

	internal string getDescr()
	{
		if (Random.Range(0, 2) == 0)
			return descrEN1;
		else
			return descrEN2;
	}
}

internal class clEmotion
{
	internal int id;
	string label;
	string descrEN;
	string winEN;

	internal clEmotion(int id, string label, string labelFR, string descrEN,  string winEN)
	{
		this.id = id;
		this.label = label;
		this.descrEN = descrEN;
		this.winEN = winEN;
	}

	internal string getId()
	{
		return label;
	}

	internal string getDescr()
	{
		return descrEN;
	}

	internal string getWin()
	{
		return winEN;
	}
}

internal class clPlace
{
	internal string textEN;
	internal string textFR;
	internal string descEN;
	internal string descFR;

	internal clPlace(string textEN, string textFR, string descEN, string descFR)
	{
		this.textEN = textEN;
		this.textFR = textFR;
		this.descEN = descEN;
		this.descFR = descFR;
	}
}

internal class clResult
{
	internal string emotionTxt;
	internal string objectTxt;
	internal int[] val;

	//private  emotion;
	//private int emotionInv = 0;
	//private clObject obj;
	internal int action;

	internal clResult(int action, string emotionTxt, string objectTxt, params int[] val)
	{
		this.action = action;
		this.emotionTxt = emotionTxt;
		this.objectTxt = objectTxt;
		this.val = val;
	}

	internal void check()
	{
		clEmotion emotion = Gvar.lstEmotion.Find(X => X.getId() == emotionTxt);

		if (emotion == null)
		{
			Debug.Log("!!!!!!!!! Emotion not found: " + emotionTxt);
			return;
		}

		clObject obj = Gvar.lstObject.Find(X => X.id == objectTxt);
		if (obj == null)
		{
			Debug.Log("!!!!!!!!! Object not found: " + objectTxt);
			return;
		}
	}
}

internal class clAnswer : System.IComparable<clAnswer>
{
	internal int id;
	internal int score;

	internal clAnswer(int id, int score)
	{
		this.id = id;
		this.score = score;
	}

	public int CompareTo(clAnswer other)
	{
		return other.score.CompareTo(score);
	}
}

internal class clScene
{
	internal static List<clHero> lstHeros;
	internal static List<clEmotion> lstEmotion;
	internal static List<clObject> lstObject;
	internal static List<clPlace> lstPlace;


	internal clAction action;
	internal clEmotion pnjEmotion;
	internal clHero pnj;
	internal clObject obj;
	internal clPlace place;

	internal string introText;

	internal static void init()
	{
		lstHeros = new List<clHero>();
		lstHeros.AddRange(Gvar.lstHeros);

		lstObject = new List<clObject>();
		lstObject.AddRange(Gvar.lstObject);

		lstPlace = new List<clPlace>();
		lstPlace.AddRange(Gvar.lstPlace);

		lstEmotion= new List<clEmotion>();
		lstEmotion.AddRange(Gvar.lstEmotion);
	}

	internal clScene()
	{
		pnj = lstHeros[Random.Range(0, lstHeros.Count)];
		pnjEmotion = lstEmotion[Random.Range(0, lstEmotion.Count)];
		obj = lstObject[Random.Range(0, lstObject.Count)];
		place = lstPlace[Random.Range(0, lstPlace.Count)];

		lstHeros.Remove(pnj);
		lstObject.Remove(obj);
		lstPlace.Remove(place);
		lstEmotion.Remove(pnjEmotion);
		action = Gvar.lstAction.Find(x => x.emotion == pnjEmotion.getId());

		if (action == null)
		{
			Debug.Log("Emotion not found in action " + pnjEmotion.getId());
			AirConsole.instance.Broadcast(Cmd.Play + "Emotion not found in action " + pnjEmotion.getId());
			return;
		}

		string actionSentence = string.Format(action.sentence, obj.getLabel());
		introText = string.Format("In {0}, {1} {2}.", place.textEN, pnj.getDescr(), actionSentence);

	}

	internal clResult getResult(clAnswer result)
	{
		return Gvar.lstResult.Find(X => X.action == result.id && X.emotionTxt == pnjEmotion.getId());
	}
}

internal enum enGameState
{
	None,
	Intro,
	Intro2,
	Play,
	Result,
	End
}


enum msgResponse
{
	None,
	NotYourTurn
}

enum enLanguage
{
	English,
	French
}