import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class SlugService {
  private readonly cyrillicMap: { [key: string]: string } = {
    'а':'a','б':'b','в':'v','г':'g','д':'d','е':'e','ж':'zh','з':'z',
    'и':'i','й':'y','к':'k','л':'l','м':'m','н':'n','о':'o','п':'p',
    'р':'r','с':'s','т':'t','у':'u','ф':'f','х':'h','ц':'ts','ч':'ch',
    'ш':'sh','щ':'sht','ъ':'a','ь':'','ю':'yu','я':'ya'
  };

  transliterate(text: string): string {
    return text
      .toLowerCase()
      .split('')
      .map(char => this.cyrillicMap[char] ?? char)
      .join('');
  }

  toFileName(text: string, fallback: string = 'file'): string
  {
    return this.toSlug(text) || fallback;
  }

  toSlug(text: string): string {
    const base = text?.trim() || '';
    return this.transliterate(base)
      .replace(/[^a-z0-9]+/g, '-')
      .replace(/^-+|-+$/g, '');
  }
}