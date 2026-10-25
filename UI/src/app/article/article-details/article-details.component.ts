import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { faTrashCan, faPenToSquare, faEye, faFolderClosed, faFolderOpen } from '@fortawesome/free-solid-svg-icons';
import { Observable } from 'rxjs';
import { ArticleDetails } from 'src/app/models/article/article-details.model';
import { User } from 'src/app/models/user/user.module';
import { ArticleService } from 'src/app/services/article/article.service';
import { AuthService } from 'src/app/services/auth/auth.service';
import { CommentService } from 'src/app/services/comment/comment.service';
import { ArticleComment } from 'src/app/models/comment/article-comment';
import { ArticleVisitsService } from 'src/app/services/article-visits/article-visits.service';
import { AdminArticleService } from 'src/app/services/article/admin-article.service';

@Component({
  selector: 'app-article-details',
  templateUrl: './article-details.component.html',
  styleUrls: ['./article-details.component.css'],
  standalone: false
})
export class ArticleDetailsComponent implements OnInit {
  user?: User;
  article: ArticleDetails | undefined;
  id: string | null = null;

  comments$: Observable<ArticleComment[]> = new Observable<ArticleComment[]>();

  faPenToSquare = faPenToSquare;
  faTrashCan = faTrashCan;
  faEye = faEye;
  faFolderClosed = faFolderClosed;
  faFolderOpen = faFolderOpen;
  hideDeleteConformation: boolean = false;
  hideMessageError: boolean = false;
  errorMessage: string = '';

  constructor(
    private articleService: ArticleService,
    private adminArticleService: AdminArticleService,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private commentService: CommentService,
    private articleVisitsService: ArticleVisitsService
  ) { }

  ngOnInit(): void {
    this.authService.user$.subscribe({
      next: (response) => {
        this.user = response;
      },
    });

    debugger;

    const isAdminView = this.route.snapshot.data['isAdminView'] === true;


    this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');
        if (this.id != null) {
          const article$ = (isAdminView
            ? this.adminArticleService.getArticleForPreview(this.id)
            : this.articleService.getArticleById(this.id)) as Observable<ArticleDetails>;

          article$.subscribe({
            next: (response) => {
              this.article = response;

              if (!this.user?.roles?.includes('Writer')) {
                this.articleVisitsService.registerVisit(this.article.id).subscribe();
              }
            },
            error: (err) => {
              this.hideMessageError = true;
              this.errorMessage = "Статията не е открита!";
            },
          });
          this.comments$ = this.commentService.getAllComments(this.id);
        }
      },
    });
  }

  get visitCount(): number {
    let totalVisitCount = this.article?.totalVisitCount ?? 0;
    if (!this.user?.roles?.includes('Writer')) {

      totalVisitCount += 1;
    }
    return totalVisitCount;
  }

  onDelete(): void {
    this.hideDeleteConformation = true;
  }

  updateArchiveStatut(isArchived: boolean): void {
    if (this.id) {
      this.adminArticleService.updateArchiveStatut(this.id, isArchived)
        .subscribe({
          next: (value) => {
            if (this.article) {
              this.article.isArchived = value;
            }
          },
          error: (err) => {
            if (err.status === 409) {
              this.errorMessage = err.error;
            }
            else {
              this.errorMessage = "Статията не е открита!";

            }
            this.hideMessageError = true;
          }
        })

    }
  }

  handleDelete(): void {
    this.hideDeleteConformation = false;

    if (this.id) {
      this.adminArticleService.deleteArticle(this.id).subscribe({
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

  handleClose(): void {
    this.hideMessageError = false;
    this.router.navigateByUrl('/articles');

  }
}
