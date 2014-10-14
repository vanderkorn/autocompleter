// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AsciiChars.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   The ASCII chars.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Core.Models
{
    /// <summary>
    /// The ASCII chars.
    /// </summary>
    public static class AsciiChars
    {
        public static AsciiChar Null { get { return new AsciiChar((byte)0); } }
        public static AsciiChar StartOfHeading { get { return new AsciiChar((byte)1); } }
        public static AsciiChar StartOfText { get { return new AsciiChar((byte)2); } }
        public static AsciiChar EndOfText { get { return new AsciiChar((byte)3); } }
        public static AsciiChar EndOfTransmission { get { return new AsciiChar((byte)4); } }
        public static AsciiChar Enquiry { get { return new AsciiChar((byte)5); } }
        public static AsciiChar Acknowledge { get { return new AsciiChar((byte)6); } }
        public static AsciiChar Bell { get { return new AsciiChar((byte)7); } }
        public static AsciiChar Backspace { get { return new AsciiChar((byte)8); } }
        public static AsciiChar HorizontalTab { get { return new AsciiChar((byte)9); } }
        public static AsciiChar LineFeed { get { return new AsciiChar((byte)10); } }
        public static AsciiChar VerticalTab { get { return new AsciiChar((byte)11); } }
        public static AsciiChar FormFeed { get { return new AsciiChar((byte)12); } }
        public static AsciiChar CarriageReturn { get { return new AsciiChar((byte)13); } }
        public static AsciiChar ShiftOut { get { return new AsciiChar((byte)14); } }
        public static AsciiChar ShiftIn { get { return new AsciiChar((byte)15); } }
        public static AsciiChar DataLinkEscape { get { return new AsciiChar((byte)16); } }
        public static AsciiChar DeviceControl1 { get { return new AsciiChar((byte)17); } }
        public static AsciiChar DeviceControl2 { get { return new AsciiChar((byte)18); } }
        public static AsciiChar DeviceControl3 { get { return new AsciiChar((byte)19); } }
        public static AsciiChar DeviceControl4 { get { return new AsciiChar((byte)20); } }
        public static AsciiChar NegativeAcknowledge { get { return new AsciiChar((byte)21); } }
        public static AsciiChar SynchronousIdle { get { return new AsciiChar((byte)22); } }
        public static AsciiChar EndOfTransmissionBlock { get { return new AsciiChar((byte)23); } }
        public static AsciiChar Cancel { get { return new AsciiChar((byte)24); } }
        public static AsciiChar EndOfMedium { get { return new AsciiChar((byte)25); } }
        public static AsciiChar Substitute { get { return new AsciiChar((byte)26); } }
        public static AsciiChar Escape { get { return new AsciiChar((byte)27); } }
        public static AsciiChar FileSeperator { get { return new AsciiChar((byte)28); } }
        public static AsciiChar GroupSeperator { get { return new AsciiChar((byte)29); } }
        public static AsciiChar RecordSeperator { get { return new AsciiChar((byte)30); } }
        public static AsciiChar UnitSeperator { get { return new AsciiChar((byte)31); } }
        public static AsciiChar Space { get { return new AsciiChar((byte)32); } }
        public static AsciiChar ExclamationMark { get { return new AsciiChar((byte)33); } }
        public static AsciiChar QuotationMark { get { return new AsciiChar((byte)34); } }
        public static AsciiChar NumberSign { get { return new AsciiChar((byte)35); } }
        public static AsciiChar DollarSign { get { return new AsciiChar((byte)36); } }
        public static AsciiChar PercentSign { get { return new AsciiChar((byte)37); } }
        public static AsciiChar Ampersand { get { return new AsciiChar((byte)38); } }
        public static AsciiChar Apostrophe { get { return new AsciiChar((byte)39); } }
        public static AsciiChar OpenParentheses { get { return new AsciiChar((byte)40); } }
        public static AsciiChar CloseParentheses { get { return new AsciiChar((byte)41); } }
        public static AsciiChar Asterisk { get { return new AsciiChar((byte)42); } }
        public static AsciiChar PlusSign { get { return new AsciiChar((byte)43); } }
        public static AsciiChar Comma { get { return new AsciiChar((byte)44); } }
        public static AsciiChar MinusSign { get { return new AsciiChar((byte)45); } }
        public static AsciiChar FullStop { get { return new AsciiChar((byte)46); } }
        public static AsciiChar FrontSlash { get { return new AsciiChar((byte)47); } }
        public static AsciiChar Zero { get { return new AsciiChar((byte)48); } }
        public static AsciiChar One { get { return new AsciiChar((byte)49); } }
        public static AsciiChar Two { get { return new AsciiChar((byte)50); } }
        public static AsciiChar Three { get { return new AsciiChar((byte)51); } }
        public static AsciiChar Four { get { return new AsciiChar((byte)52); } }
        public static AsciiChar Five { get { return new AsciiChar((byte)53); } }
        public static AsciiChar Six { get { return new AsciiChar((byte)54); } }
        public static AsciiChar Seven { get { return new AsciiChar((byte)55); } }
        public static AsciiChar Eight { get { return new AsciiChar((byte)56); } }
        public static AsciiChar Nine { get { return new AsciiChar((byte)57); } }
        public static AsciiChar Colon { get { return new AsciiChar((byte)58); } }
        public static AsciiChar Semicolon { get { return new AsciiChar((byte)59); } }
        public static AsciiChar LessThanSign { get { return new AsciiChar((byte)60); } }
        public static AsciiChar EqualsSign { get { return new AsciiChar((byte)61); } }
        public static AsciiChar GreaterThanSign { get { return new AsciiChar((byte)62); } }
        public static AsciiChar QuestionMark { get { return new AsciiChar((byte)63); } }
        public static AsciiChar AtSign { get { return new AsciiChar((byte)64); } }
        public static AsciiChar A { get { return new AsciiChar((byte)65); } }
        public static AsciiChar B { get { return new AsciiChar((byte)66); } }
        public static AsciiChar C { get { return new AsciiChar((byte)67); } }
        public static AsciiChar D { get { return new AsciiChar((byte)68); } }
        public static AsciiChar E { get { return new AsciiChar((byte)69); } }
        public static AsciiChar F { get { return new AsciiChar((byte)70); } }
        public static AsciiChar G { get { return new AsciiChar((byte)71); } }
        public static AsciiChar H { get { return new AsciiChar((byte)72); } }
        public static AsciiChar I { get { return new AsciiChar((byte)73); } }
        public static AsciiChar J { get { return new AsciiChar((byte)74); } }
        public static AsciiChar K { get { return new AsciiChar((byte)75); } }
        public static AsciiChar L { get { return new AsciiChar((byte)76); } }
        public static AsciiChar M { get { return new AsciiChar((byte)77); } }
        public static AsciiChar N { get { return new AsciiChar((byte)78); } }
        public static AsciiChar O { get { return new AsciiChar((byte)79); } }
        public static AsciiChar P { get { return new AsciiChar((byte)80); } }
        public static AsciiChar Q { get { return new AsciiChar((byte)81); } }
        public static AsciiChar R { get { return new AsciiChar((byte)82); } }
        public static AsciiChar S { get { return new AsciiChar((byte)83); } }
        public static AsciiChar T { get { return new AsciiChar((byte)84); } }
        public static AsciiChar U { get { return new AsciiChar((byte)85); } }
        public static AsciiChar V { get { return new AsciiChar((byte)86); } }
        public static AsciiChar W { get { return new AsciiChar((byte)87); } }
        public static AsciiChar X { get { return new AsciiChar((byte)88); } }
        public static AsciiChar Y { get { return new AsciiChar((byte)89); } }
        public static AsciiChar Z { get { return new AsciiChar((byte)90); } }
        public static AsciiChar OpenSquareBrackets { get { return new AsciiChar((byte)91); } }
        public static AsciiChar Backslash { get { return new AsciiChar((byte)92); } }
        public static AsciiChar CloseSquareBrackets { get { return new AsciiChar((byte)93); } }
        public static AsciiChar Caret { get { return new AsciiChar((byte)94); } }
        public static AsciiChar Underscore { get { return new AsciiChar((byte)95); } }
        public static AsciiChar GraveAccent { get { return new AsciiChar((byte)96); } }
        public static AsciiChar a { get { return new AsciiChar((byte)97); } }
        public static AsciiChar b { get { return new AsciiChar((byte)98); } }
        public static AsciiChar c { get { return new AsciiChar((byte)99); } }
        public static AsciiChar d { get { return new AsciiChar((byte)100); } }
        public static AsciiChar e { get { return new AsciiChar((byte)101); } }
        public static AsciiChar f { get { return new AsciiChar((byte)102); } }
        public static AsciiChar g { get { return new AsciiChar((byte)103); } }
        public static AsciiChar h { get { return new AsciiChar((byte)104); } }
        public static AsciiChar i { get { return new AsciiChar((byte)105); } }
        public static AsciiChar j { get { return new AsciiChar((byte)106); } }
        public static AsciiChar k { get { return new AsciiChar((byte)107); } }
        public static AsciiChar l { get { return new AsciiChar((byte)108); } }
        public static AsciiChar m { get { return new AsciiChar((byte)109); } }
        public static AsciiChar n { get { return new AsciiChar((byte)110); } }
        public static AsciiChar o { get { return new AsciiChar((byte)111); } }
        public static AsciiChar p { get { return new AsciiChar((byte)112); } }
        public static AsciiChar q { get { return new AsciiChar((byte)113); } }
        public static AsciiChar r { get { return new AsciiChar((byte)114); } }
        public static AsciiChar s { get { return new AsciiChar((byte)115); } }
        public static AsciiChar t { get { return new AsciiChar((byte)116); } }
        public static AsciiChar u { get { return new AsciiChar((byte)117); } }
        public static AsciiChar v { get { return new AsciiChar((byte)118); } }
        public static AsciiChar w { get { return new AsciiChar((byte)119); } }
        public static AsciiChar x { get { return new AsciiChar((byte)120); } }
        public static AsciiChar y { get { return new AsciiChar((byte)121); } }
        public static AsciiChar z { get { return new AsciiChar((byte)122); } }
        public static AsciiChar OpenBraces { get { return new AsciiChar((byte)123); } }
        public static AsciiChar VerticalBar { get { return new AsciiChar((byte)124); } }
        public static AsciiChar CloseBraces { get { return new AsciiChar((byte)125); } }
        public static AsciiChar Tilde { get { return new AsciiChar((byte)126); } }
        public static AsciiChar Delete { get { return new AsciiChar((byte)127); } }
    }
}