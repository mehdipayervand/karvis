﻿using Karvis.Domain.ViewModels;

namespace Karvis.Domain.Tasks
{
    public interface ISearchTask
    {
        SearchViewModel GetRawModel();
        SearchResultViewModel Search(SearchViewModel searchViewModel);
    }
}