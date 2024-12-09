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
  faFishFins,
  IconDefinition,
  faBowlRice,
  faBottleDroplet,
  faHandHoldingDroplet,
  faHotTubPerson,
  faHandsHoldingCircle,
} from '@fortawesome/free-solid-svg-icons';
import { faPagelines } from '@fortawesome/free-brands-svg-icons';

@Component({
  selector: 'app-awesome-icons',
  templateUrl: './awesome-icons.component.html',
  styleUrls: ['./awesome-icons.component.css'],
})
export class AwesomeIconsComponent {
  @Input() selectedIcon: string = 'faXmark';

  icons = {
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
    faPagelines: faPagelines,
    faFishFins: faFishFins,
    faBowlRice: faBowlRice,
    faBottleDroplet: faBottleDroplet,
    faHandHoldingDroplet: faHandHoldingDroplet,
    faHotTubPerson: faHotTubPerson,
    faHandsHoldingCircle: faHandsHoldingCircle,
  };

  getIcon(iconKey: string): IconDefinition {
    return this.icons[iconKey as keyof typeof this.icons] || faXmark;
  }
}
