import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { faPenToSquare, faTrashCan } from '@fortawesome/free-regular-svg-icons';
import { faPen } from '@fortawesome/free-solid-svg-icons';
import { PostList } from 'src/app/models/instagram/post/post-list.model';
import { PostService } from 'src/app/services/post/post.service';

@Component({
  selector: 'app-posts-list',
  templateUrl: './posts-list.component.html',
  styleUrl: './posts-list.component.css',
  standalone: false,
})
export class PostsListComponent implements OnInit {
  posts: PostList[] = [];
  faPen = faPen;
  faPenToSquare = faPenToSquare;
  faTrashCan = faTrashCan;

  constructor(private postService: PostService, private router: Router){}

  ngOnInit():void 
  {
    this.postService.getAllPosts().subscribe({
      next: (posts) => {
        this.posts = posts;
      },
      error: (err) => {
        console.error(err);
      }
    });
  }

  addPost(): void {
    this.router.navigateByUrl('/admin/instagram/post');
  }

  onDelete(id: string): void {

  }

}
