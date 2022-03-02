using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;

namespace ShopManagement.Domain.SlideAgg {
    public class Slide : EntityBase{
        public string? Picture { get; private set; }
        public string? PictureAlt { get; set; }
        public string? PictureTitle { get; set; }
        public string? Heading { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public string? BtnText { get; set; }
        public bool IsRemoved { get; set; }

        public Slide(string? picture, string? pictureAlt, string? pictureTitle, string? heading, string? title, string? text, string? btnText) {
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Heading = heading;
            Title = title;
            Text = text;
            BtnText = btnText;
            IsRemoved = false;
        }

        public void Edit(string? picture, string? pictureAlt, string? pictureTitle, string? heading, string? title, string? text, string? btnText) {
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Heading = heading;
            Title = title;
            Text = text;
            BtnText = btnText;
        }

        public void Remove() {
            IsRemoved=true;
        }

        public void Restore() {
            IsRemoved=false;
        }
    }
}
