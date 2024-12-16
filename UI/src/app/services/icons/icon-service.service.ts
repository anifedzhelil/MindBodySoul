import { Injectable } from '@angular/core';
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

@Injectable({
  providedIn: 'root',
})
export class IconsService {
  private icons = {
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

  getIcons() {
    return this.icons;
  }

  getIconKeys() {
    return Object.keys(this.icons);
  }
}
