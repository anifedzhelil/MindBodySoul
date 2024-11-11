import { Component, Input } from '@angular/core';
import {
  faHeart,
  faLeaf,
  faAppleAlt,
  faSun,
  faSeedling,
  faAtom,
  faWind,
  faFeatherPointed,
  faFire,
  faMortarPestle,
  faBrain,
  faXmark,
  IconDefinition,
} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-awesome-icons',
  templateUrl: './awesome-icons.component.html',
  styleUrls: ['./awesome-icons.component.css'],
})
export class AwesomeIconsComponent {
  @Input() selectedIcon: string  = 'faXmark';
  
  icons= {
    faHeart: faHeart,
    faLeaf: faLeaf,
    faAppleAlt: faAppleAlt,
    faSun: faSun,
    faSeedling: faSeedling,
    faAtom: faAtom,
    faWind: faWind,
    faFeatherPointed: faFeatherPointed,
    faFire: faFire,
    faMortarPestle: faMortarPestle,
    faBrain: faBrain,
  };

  getIcon(iconKey: string): IconDefinition  {
    return this.icons[iconKey as keyof typeof this.icons] || faXmark;
  }
}
