    using Prototypes.Model.Interfaces;
    using Prototypes.UI;
    using System.Collections.ObjectModel;

    namespace Prototypes.Model;

    public class SearchBarPerformersOfSongsViewModel : ObservableObject
    {
        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                SetProperty(ref _searchText, value);
                FilterNames();
            }
        }


        private ObservableCollection<Performer> _performers;
        public ObservableCollection<Performer> Performers
        {
            get { return _performers; }
            set { SetProperty(ref _performers, value); }
        }

        private ObservableCollection<Performer> _filteredPerformers;
        public ObservableCollection<Performer> FilteredPerformers
        {
            get { return _filteredPerformers; }
            set { SetProperty(ref _filteredPerformers, value); }
        }

        public SearchBarPerformersOfSongsViewModel(Song song)
        {
            Performers = MauiProgram.BusinessLogic.GetPerformersOfASong(song);
            FilteredPerformers = Performers;
        }

        private void FilterNames()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                FilteredPerformers = Performers;
            }
            else
            {
                var searchWords = SearchText.Trim().Split(' ');

                FilteredPerformers = new ObservableCollection<Performer>(
                 Performers.Where(performer =>
                     searchWords.All(word =>
                         performer.FirstName.ToLower().Contains(word.ToLower()) ||
                         performer.LastName.ToLower().Contains(word.ToLower())
                     )
                 )
             );
            }
        }
    }
