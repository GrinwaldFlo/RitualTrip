using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using System;

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

	internal static void setGameState(enGameState state)
	{
		gameState = state;
	}

	internal static string addDet(string val)
	{
		if(lng == enLanguage.English)
		{
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
		return val;
	}

	internal static void init()
	{
		lstHeros = new List<clHero>();
		lstEmotion = new List<clEmotion>();
		lstPlace = new List<clPlace>();
		lstAction = new List<clAction>();
		lstObject = new List<clObject>();

		lstPlace.Add(new clPlace("a good old dungeon", "donjon", "You just arrived in a closed, dark and humid place... some screams can be heard... some of them are fare away, some of them are not. You can feel a massiv building above you. You are in a good old dungeon full of stones, pain and desillusion.", ""));
		lstPlace.Add(new clPlace("an intergalactic station", "base sidérale", "In your guts you know you are not on Earth anymore. But...this is not a moon this is a intergalactic station ! Your presence is forbidden, you know you shouldn't stay here for long...", ""));
		lstPlace.Add(new clPlace("an enchanted forest", "forêt enchantée", "Some  birds sounds, wind in the leaves... and feminin laughts everywhere... maybe in your minds... this forest is not an ordinary one. Something in the woods is looking at you... something in the woods is waiting for you.", ""));



		lstObject.Add(new clObject("a rotting dead body", "cadavre"));
		lstObject.Add(new clObject("a mysterious altar", "autel"));
		lstObject.Add(new clObject("a rubber chicken", "poulet en caoutchouc"));
		lstObject.Add(new clObject("a regular light saber", "sabre laser"));
		lstObject.Add(new clObject("a chicken sandwich", "sandwich au poulet"));
		lstObject.Add(new clObject("a mobile phone", "natel"));
		lstObject.Add(new clObject("a flower", "fleur"));
		lstObject.Add(new clObject("a hamster", "hamster"));
		lstObject.Add(new clObject("a chandelier", "chandelier"));



		lstAction.Add(new clAction("depressive", "is comiting suicide with {0}", "It is a respectfull choice, let's make it quicker, it is obvious that this person doesn't know to do it well.", "Let's safe this life ! No one could be let alone in this moment of mind loss.", "Nothing to win, let's do nothing.", "Quicker is better, you a let another dead body behind you and slide in another world.", "Another life saved, what can bring more satisfaction ? Let's hope the next world will be less desesperate.", ""));
		lstAction.Add(new clAction("sadistic", "tortures {0}", "Le'ts have fun, join the party ! Which part is still untouched ?", "No way... this must comes to an end right now !", "Not your business, let's go to another world.", "This was soooo fun, you can't wait for another world, full of such nice moments...", "Damn lunatics... Hoping that one day the victim will be able to recover  you get throught the Ritual portal with a deep sens of accomplishment.", ""));
		lstAction.Add(new clAction("lubric", "takes pleasure with {0}", "Fun can always be shared. You never know when it will be the last time...", "Let's stop this, this is insane.", "Weird... but we don't care.", "Exhausted and smilly, you succeed to finally decide to keep on the Ritual.", "Some acts shouldn't be accomplished... Just BECAUSE ! Now jump to another universe, hopefully less disgusting.", ""));
		lstAction.Add(new clAction("despotic", "imperially controls {0}", "Better to be on the ruler's side of social life... You even can enjoy it !", "This is unfair, let's stop this.", "Some of them wants to use u, some of them wants to get used by u... nothing new, let's get out of here.", "Such a nice feeling to be on the bright side of autorithy. Proud of yourself you enter in a new world. ", "No one should endure such a pressure... NO PASARAN ! Time to jump.", "Politics ? WTF, we are just tourists here..."));
		lstAction.Add(new clAction("ecstatic", "is amazed by {0}", "This is fantastic, we must say something about it, let's write a poem.", "This is stupid... we can't let this happening", "WHO CARES ?", "Universe is a perfect place, not a day should be wasted by not saying it. Time to open a new cool page of your story now...", "Nonsens is nonsens. (Hit the singer till it STFU) Universe is more coherent now. Ready to jump.", "Reality is not nice, or nice. It is. Nothing to enjoy or to be depleased of."));
		lstAction.Add(new clAction("peaceful", "sings {0}'s praises", "Peace on earth, spread the message... We may be dreamers, but we are not the only ones.", "Fucking hippy... open your eyes to the real world !", "... really ? NEXT....", "What a bliss... cheers and join the dance !", "Siliness is an insult to reality. Let's make all the useless and disturbing singers to shut the F... UP ! ", "We are deaf ok ? we don't hear anything... let's go."));
		lstAction.Add(new clAction("puritan", "lectures {0}", "The right way must be explained and pointed right now ! This is our duty !", "So many stupid words... this must be corrected.", "nonsens... next world.", "No one can negate your deep sens morality now. Time to spread the good word in another space-time.", "No one should decide and impose his so called right way, if truth exsts it doesn't need defenders.", "The strong rules the weak... eternal story, boring..."));
		lstAction.Add(new clAction("idealist", "leads a riot in the name of {0}", "Get up ! Stand up for your rights ! Freedom is just ahead !", "Damn anarchists... let's defend order against those silly idealists ! You shall not pass !", "This is not our war.", "Freedom is a fight, it has a price. Let's run to another chance to defend it ! ", "Society needs order, some of us needs to do the bad job to protect the stability.", "No one cares... let's not get involve in that."));



		lstEmotion.Add(new clEmotion("depressive", "ecstatic"));
		lstEmotion.Add(new clEmotion("sadistic", "peaceful"));
		lstEmotion.Add(new clEmotion("lubric", "puritan"));
		lstEmotion.Add(new clEmotion("despotic", "idealist"));



		lstHeros.Add(new clHero("paladin", "paladin"));
		lstHeros.Add(new clHero("plumber", "plombier"));
		lstHeros.Add(new clHero("trekker", "trekker"));
		lstHeros.Add(new clHero("accountant", "comptable"));
		lstHeros.Add(new clHero("murderer", "assassin"));
		lstHeros.Add(new clHero("screenwriter", "scénariste"));
		lstHeros.Add(new clHero("nurse", "infirmière"));
		lstHeros.Add(new clHero("necromancer", "nécromancien"));
		lstHeros.Add(new clHero("soldier", "soldat"));
		lstHeros.Add(new clHero("unicorn", "licorne"));

		check();
	}

	private static void check()
	{
		foreach (clEmotion item in lstEmotion)
		{
			for (int i = 0; i < 2; i++)
			{
				clAction action = Gvar.lstAction.Find(x => x.emotion == item.getText(i));
				if (action == null)
				{
					Debug.Log("`!!!!!!!! Emotion not found in action " + item.getText(i));
				}
			}
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

}

internal static class Lng
{
	internal const string Intro = "From Game Press start to begin";
	internal const string Play = "From Game Play";// Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris consectetur tincidunt mollis. Nullam convallis tortor ex, id feugiat risus commodo in. Phasellus sagittis laoreet sem a mattis. Nulla lacus lacus.";
	internal const string Wait = "From Game Please wait the next game";

	internal const string Answer1 = "From Game Answer 1";
	internal const string Answer2 = "From Game Answer 2";
	internal const string Answer3 = "From Game Answer 3";
	internal const string Answer4 = "From Game Answer 4";

	internal const string YourTurn = "Your turn";
	internal const string TellFirstHelp = "Glyph only glyphs that have not yet been glyphed";
	internal const string Win = "{0} wins with {1} points";
	internal const string Lose = "No winner, shame on you !";
	internal const string NotYourTurn = "It's not your turn";
	internal const string NotAGlyph = "???";
	internal const string GlyphMissed = "Glyph missed\r\n{0} remaining tries";
	internal const string GlyphOK = "Good !";
	internal const string GlyphRemaining = "{0} glyphs remaining";
	internal const string AlreadyWritten = "Already written !\r\n{0} remaining tries";
	internal const string GlyphMissedList = "Here are the glyphs you missed";
	internal const string NotASequence = "This is not a valid sequence or it's already been found !\r\n{0} remaining tries";
	internal const string SequenceOK = "Good sequence!\r\nLet's start a new one";
	internal const string SequenceRemaining = "{0} sequences remaining";
	internal const string SequenceGlyphOK = "Good!\r\nGlyph the rest of the sequence";
	internal const string SequenceLose = "You lost, here is an existing sentence";
	internal const string SequenceStartWith = "{0} sequences start with these glyphs";
}

internal class clObject
{
	string labelEn;
	string labelFr;

	internal clObject(string labelEn, string labelFr)
	{
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
}

internal class clHero
{
	string labelEn;
	string labelFr;

	internal clHero(string labelEn, string labelFr)
	{
		this.labelEn = labelEn;
		this.labelFr = labelFr;
	}

	internal string getLabel()
	{
		return labelEn;
	}
}

internal class clEmotion
{
	string label;
	string labelInv;

	internal clEmotion(string label, string labelInv)
	{
		this.label = label;
		this.labelInv = labelInv;
	}

	internal string getText(int emotionSide)
	{
		if (emotionSide == 1)
			return labelInv;
		return label;
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