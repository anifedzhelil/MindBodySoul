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
import { IconsService } from 'src/app/services/icons/icon-service.service';

@Component({
    selector: 'app-awesome-icons',
    templateUrl: './awesome-icons.component.html',
    styleUrls: ['./awesome-icons.component.css'],
    standalone: false
})
export class AwesomeIconsComponent {
  @Input() selectedIcon: string = 'faXmark';

  constructor(private iconsService: IconsService) {}

  icons = this.iconsService.getIcons();

  getIcon(iconKey: string): IconDefinition {
    return this.icons[iconKey as keyof typeof this.icons] || faXmark;
  }
}
