﻿using _0_Framework.Application;

namespace BlogManagement.Application.Contract.ArticleCategory {
    public interface IArticleCategoryApplication {
        OperationResult Create (CreateArticleCategory command);
        OperationResult Edit (EditArticleCategory command);
        EditArticleCategory GetDetails (long id);
        List<ArticleCategoryViewModel> Search (ArticleCategorySearchModel searchModel);
        List<ArticleCategoryViewModel> GetArticleCategories();
    }
}
