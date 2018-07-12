using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace GameManager
{
    class MeniuVM : BaseVM
    {
        private string image;
        //private List<string> images;
        //private int index;

        private User currentUser;
        public ObservableCollection<User> listOfUsers;

        public ObservableCollection<User> ListOfUsers
        {
            get
            {
                return listOfUsers;
            }

            set
            {
                listOfUsers = value;
                OnPropertyChanged("ListOfUsers");
            }
        }
        public string Image
        {
            get
            {
                return image;
            }

            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }
        public int Index { get; set; }
        public List<string> Images { get; set; }
        public User CurrentUser
        {
            get
            {
                return currentUser;
            }
            set
            {
                currentUser = value;
                Image = currentUser.Image;
                OnPropertyChanged("CurrentUser");
            }
        }

        public MeniuVM()
        {
            Index = -1;
            Images = new List<string>();
            Images.Add("../Resources/img1.jpg");
            Images.Add("../Resources/img2.jpg");
            Images.Add("../Resources/img3.jpg");
            Images.Add("../Resources/img4.jpg");

            ListOfUsers = new ObservableCollection<User>();

            /*ListOfUsers.Add(new User("Dora", Images[0],false));
            ListOfUsers.Add(new User("Ioana", Images[1],false));
            ListOfUsers.Add(new User("Mihai", Images[2],false));
            ListOfUsers.Add(new User("Maimuta", Images[3],false));
            ListOfUsers.Add(new User("Alice", Images[2],false));*/

            //ListOfUsers = Serialize.DeserializeObject<ObservableCollection<User>>("C:\\Users\\Isus te iubeste\\Documents\\visual studio 2010\\Projects\\Pairs\\Pairs\\bin\\Debug\\users.xml");



        }

    }
}
