import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { faTrashCan, faPenToSquare, faEye } from '@fortawesome/free-solid-svg-icons';
import { Observable } from 'rxjs';
import { ArticleDetails } from 'src/app/models/article/article-details.model';
import { User } from 'src/app/models/user/user.module';
import { ArticleService } from 'src/app/services/article/article.service';
import { AuthService } from 'src/app/services/auth/auth.service';
import { CommentService } from 'src/app/services/comment/comment.service';
import { ArticleComment } from 'src/app/models/comment/article-comment';
import { ArticleVisitsService } from 'src/app/services/article-visits/article-visits.service';

@Component({
  selector: 'app-article-details',
  templateUrl: './article-details.component.html',
  styleUrls: ['./article-details.component.css'],
})
export class ArticleDetailsComponent implements OnInit {
  user?: User;
  article: ArticleDetails | undefined;
  id: string | null = null;

  comments$: Observable<ArticleComment[]> = new Observable<ArticleComment[]>();

  faPenToSquare = faPenToSquare;
  faTrashCan = faTrashCan;
  faEye = faEye;

  hideDeleteConformation: boolean = false;

  constructor(
    private articleService: ArticleService,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private commentService: CommentService,
    private articleVisitsService: ArticleVisitsService
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

              if(!this.user?.roles?.includes('Writer')){
                this.articleVisitsService.registerVisit(this.article.id).subscribe();
              }
            },
            error: (err) => {
              console.error(err);
            },
          });
          this.comments$ = this.commentService.getAllComments(this.id);
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
        },
      });
    }
  }

  updateComments(): void {
    if (this.id) {
      this.comments$ = this.commentService.getAllComments(this.id);
    }
  }

  handleCancel(): void {
    this.hideDeleteConformation = false;
  }
}
