import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { faTrashCan, faPenToSquare } from '@fortawesome/free-solid-svg-icons';
import { ArticleDetails } from 'src/app/models/article/article-details.model';
import { User } from 'src/app/models/user/user.module';
import { ArticleService } from 'src/app/services/article/article.service';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-article-details',
  templateUrl: './article-details.component.html',
  styleUrls: ['./article-details.component.css'],
})
export class ArticleDetailsComponent implements OnInit {
  user?: User;
  article: ArticleDetails | undefined;
  id: string | null = null;

  faPenToSquare = faPenToSquare;
  faTrashCan = faTrashCan;

  hideDeleteConformation: boolean = false;

  constructor(
    private articleService: ArticleService,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.authService.user$.subscribe({
      next: (response) => {
        this.user = response;
      },
    });

    this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');
        if (this.id != null) {
          this.articleService.getArticleById(this.id).subscribe({
            next: (response) => {
              this.article = response;
            },
            error: (err) => {
              console.error(err);
            },
          });
        }
      },
    });
  }

  onDelete(): void {
    this.hideDeleteConformation = true;
  }

  handleDelete(): void {
    this.hideDeleteConformation = false;

    if (this.id) {
      this.articleService.deleteArticle(this.id).subscribe({
        next: (response) => {
          this.router.navigateByUrl('/articles');
          
        }
      });
    }
  }

  
  handleCancel(): void {
    this.hideDeleteConformation = false;
  }
}
