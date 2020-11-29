namespace AutoVersionsDB.UI.DBVersions
{
    public delegate TextInputResults OnTextInputEventHandler(object sender, string instructionMessageText);
    public class TextInputResults
    {
        public string ResultText { get; set; }
        public bool IsApply { get; set; }
    }

}
