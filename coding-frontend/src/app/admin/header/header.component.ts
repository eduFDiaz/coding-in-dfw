import { Component, OnInit, Input } from '@angular/core';
import { NbMediaBreakpointsService, NbMenuService, NbSidebarService, NbThemeService, } from '@nebular/theme';

import { map, takeUntil } from 'rxjs/operators';
import { Subject, Observable } from 'rxjs';
import { UserService } from '../../_services/user.service'
import { AuthService } from '../../_services//auth.service';
import { AlertService } from '../../_services/alert.service'
import { Router } from '@angular/router';
import { User } from '../../_models/User';
import { Photo } from 'src/app/_models/Photo';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.sass']
})
export class HeaderComponent implements OnInit {

  private destroy$: Subject<void> = new Subject<void>();


  theuser: User

  userPictureOnly = false;

  avatarUrl: any

  isLoggedIn: boolean

  currentUser: User

  userPhotos: Photo[]

  // currentAvatarUrl = ''

  userMenu = [{ title: 'Profile', icon: 'person' }, { title: 'Log out', icon: 'power-outline' }];
  photoUrl: string;
  currentAvatarUrl: string;

  constructor(private sidebarService: NbSidebarService, private menuService: NbMenuService,
    private themeService: NbThemeService,
    private breakpointService: NbMediaBreakpointsService,
    private user: UserService,
    private auth: AuthService,
    private router: Router,
    private toast: AlertService

  ) {
    // this.currentUser = this.auth.getUser()
    // this.auth.getUser().subscribe(x => this.currentUser = x)


    this.user.getAllUserPhotos().subscribe((photo: any) => {
      this.userPhotos = photo
      this.currentAvatarUrl = this.userPhotos.filter(photo => photo.isMain == true).map(photo => photo.url).toString()
    })

    this.auth.currentPhotoUrl.subscribe(photoUrl => this.photoUrl = photoUrl);

  }


  ngOnInit() {

    this.menuService.onItemClick().subscribe((event) => {
      this.onItemSelection(event.item.title);
    });

    const { xl } = this.breakpointService.getBreakpointsMap();

    this.themeService.onMediaQueryChange()
      .pipe(
        map(([, currentBreakpoint]) => currentBreakpoint.width < xl),
        takeUntil(this.destroy$),
      )
      .subscribe((isLessThanXl: boolean) => this.userPictureOnly = isLessThanXl);

    this.theuser = this.auth.currentUserValue
    this.isLoggedIn = this.auth.loginStatusValue
    // this.auth.currentLoginStatus.subscribe(result => this.isLoggedIn = result)
    // this.auth.currentUser.subscribe(user => this.theuser = user)


    console.log(this.auth.currentUserValue)
    console.log(this.isLoggedIn)
  }

  onItemSelection(title) {
    if (title === 'Log out') {
      this.logout()

    } else if (title === 'Profile') {

    }
  }

  toggleSidebar(): boolean {
    this.sidebarService.toggle(true, 'menu-sidebar')
    // this.layoutService.changeLayoutSize();
    return false;
  }

  navigateHome() {
    this.menuService.navigateHome()
    return false;
  }


  logout() {
    this.auth.logout()
    // this.toast.showToast('bottom-left', 'info', 'See you soon!', 'You have sucefully logout')
    // location.reload(true);
    this.router.navigate(['/'])

  }

}

