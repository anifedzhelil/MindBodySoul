import { Injectable, Input } from '@angular/core';
import {
  faHeart, faHeartPulse, faBrain, faBone, faEye, faBacteria,
  faTooth, faLungs, faStethoscope, faDroplet, faFire,
  faLeaf, faSeedling, faTree, faSun, faSnowflake,
  faWind, faFeatherPointed, faAppleAlt, faBowlRice,
  faFishFins, faMugHot, faBottleDroplet, faJar,
  faWheatAwn, faMortarPestle, faHandHoldingDroplet,
  faHandsHoldingCircle, faHandSparkles, faSpa,
  faYinYang, faPersonPraying, faDumbbell, faStar,
  faCircleInfo, faBookOpen, faFlask, faAtom,
  faHotTubPerson, IconDefinition
} from '@fortawesome/free-solid-svg-icons';
import { faPagelines } from '@fortawesome/free-brands-svg-icons';

@Injectable({
  providedIn: 'root',
})
export class IconsService {
  private icons: Record<string, IconDefinition> = {
    // Health and body
    faHeart, faHeartPulse, faBrain, faBone, faEye, faBacteria,
    faTooth, faLungs, faStethoscope, faDroplet, faFire,
    // Nature and plants
    faLeaf, faSeedling, faTree, faSun, faSnowflake,
    faWind, faFeatherPointed, faPagelines,
    // Food and drinks
    faAppleAlt, faBowlRice, faFishFins, faMugHot,
    faBottleDroplet, faJar, faWheatAwn,
    // Therapies and healing
    faMortarPestle, faHandHoldingDroplet, faHandsHoldingCircle,
    faHandSparkles, faSpa, faYinYang, faPersonPraying,
    faDumbbell, faHotTubPerson,
    // General
    faStar, faCircleInfo, faBookOpen, faFlask, faAtom,
  };

  @Input() mode: 'picker' | 'display' = 'picker';

  getIcons(): Record<string, IconDefinition> {
    return this.icons;
  }

  getIconKeys(): string[] {
    return Object.keys(this.icons);
  }
}