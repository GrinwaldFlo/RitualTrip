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

	internal static enGameState gameState { get; private set; }

	internal static void setGameState(enGameState state)
	{
		gameState = state;
	}
}

internal static class Cmd
{
	internal const string Intro = "#Intro";
	internal const string IntroText = "#IntroText|";
	internal const string Play = "#Play";
	internal const string PlayText = "#PlayText|";
	internal const string End = "#End";
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


internal enum enGameState
{
	None,
	Intro,
	Play,
	End
}


enum msgResponse
{
	None,
	NotYourTurn
}