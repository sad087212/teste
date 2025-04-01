namespace FIPECAFI.Models.Modal;

public class ModalModel
{
    public string ModalId { get; set; }
    public string ModalIdLabel { get; set; }
    public string Title { get; set; }
    public bool ShowCloseButton { get; set; } = true;
    public bool ShowSaveButton { get; set; } = true;
    public string OnSaveAction { get; set; }
}