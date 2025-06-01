using Bytagramm.Dto;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bytagramm.ViewModels
{
    public partial class ProfileVewModel : ObservableObject
    {
        [ObservableProperty]
        public string name;

        [ObservableProperty]
        public string email;

        [ObservableProperty]
        public List<CommunityDto> communities;

        [ObservableProperty]
        public List<PostDto> posts;


    }
}
