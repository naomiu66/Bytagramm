using Bytagramm.Dto.Community;
using Bytagramm.Services.Abstractions;
using Bytagramm.Views.Community;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Bytagramm.ViewModels
{
    public partial class CommunitiesViewModel : ObservableObject
    {
        private readonly ICommunityApiService _communityApiService;

        public CommunitiesViewModel(ICommunityApiService communityApiService)
        {
            _communityApiService = communityApiService;

            Communities = new ObservableCollection<CommunityDto>();
            LoadPageContent();
        }

        public ObservableCollection<CommunityDto> Communities { get; }

        [RelayCommand]
        private async Task OpenCommunity(CommunityDto selectedCommunity) 
        {
            if (selectedCommunity is not null)
            {
                await Shell.Current.GoToAsync($"{nameof(CommunityDetailsPage)}?communityId={selectedCommunity.Id}");
            }
        }

        private async void LoadPageContent() 
        {
            var communities = await _communityApiService.GetAllAsync();

            if (communities != null)
            {
                Communities.Clear();
                foreach (var community in communities) 
                {
                    Communities.Add(community);
                }
            }
        }
    }
}
