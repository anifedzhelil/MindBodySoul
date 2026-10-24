import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AdminArticleListItem } from 'src/app/models/article/admin-article-list-item.model';
import { ArticleStatusFilter } from 'src/app/models/article/article-status-filter.model';
import { AdminArticleService } from 'src/app/services/article/admin-article.service';

@Component({
  selector: 'app-admin-articles-list',
  templateUrl: './admin-articles-list.component.html',
  styleUrl: './admin-articles-list.component.css',
  standalone: false
})
export class AdminArticlesListComponent implements OnInit {
  selectedFilter: ArticleStatusFilter = ArticleStatusFilter.All;
  articles: AdminArticleListItem[] = [];

  sortColumn: 'title' | 'createdDate' | 'publishedDate' | 'uniqueVisitCount' = 'createdDate';
  sortDirection: 'asc' | 'desc' = 'desc';

  filters = [
    { label: 'Всички', value: ArticleStatusFilter.All },
    { label: 'Чернови', value: ArticleStatusFilter.Draft },
    { label: 'Публикувани', value: ArticleStatusFilter.Published },
    { label: 'Архивирани', value: ArticleStatusFilter.Archived }
  ];

  constructor(private adminArticleService: AdminArticleService,
    private router: Router) { }

  ngOnInit(): void {
    this.loadArticles();
  }

  addArticle(): void {
    this.router.navigateByUrl('/articles/add-article');
  }

  loadArticles(): void {
    this.adminArticleService.getFilteredForAdmin(this.selectedFilter)
      .subscribe({
        next: (response) => {
          this.articles = response;
          this.sortArticles();
        }
      })
  }

  onFilterChange(filter: ArticleStatusFilter): void {
    // Do nothing if the active tab is clicked again
    if (filter === this.selectedFilter) {
      return;
    }

    this.selectedFilter = filter;
    this.loadArticles();
  }

  getStatusLabel(article: AdminArticleListItem): string {
    // Archived is checked first, because an archived article keeps IsPublished = true
    if (article.isArchived) {
      return 'Архивирана';
    }

    return article.isPublished ? 'Публикувана' : 'Чернова';
  }

  onSort(column: 'title' | 'createdDate' | 'publishedDate' | 'uniqueVisitCount'): void {
    if (this.sortColumn === column) {
      // Same column clicked again, so only the direction flips
      this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortColumn = column;
      this.sortDirection = 'asc';
    }

    this.sortArticles();
  }

  sortArticles(): void {
    const direction = this.sortDirection === 'asc' ? 1 : -1;

    this.articles.sort((a, b) => {
      switch (this.sortColumn) {
        case 'title':
          return a.title.localeCompare(b.title, 'bg') * direction;

        case 'uniqueVisitCount':
          return (a.uniqueVisitCount - b.uniqueVisitCount) * direction;

        case 'createdDate':
          return (new Date(a.createdDate).getTime() - new Date(b.createdDate).getTime()) * direction;

        case 'publishedDate':
          // Nulls always go to the bottom, whatever the direction is
          if (!a.publishedDate && !b.publishedDate) return 0;
          if (!a.publishedDate) return 1;
          if (!b.publishedDate) return -1;
          return (new Date(a.publishedDate).getTime() - new Date(b.publishedDate).getTime()) * direction;
      }
    });
  }
}
