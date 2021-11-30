import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Observable, of } from 'rxjs';
import { UserService } from 'src/app/shared/services/user.service';

import { RatingComponent } from './rating.component';

describe('RatingComponent', () => {
  let component: RatingComponent;
  let fixture: ComponentFixture<RatingComponent>;
  let userService: UserService;
  const testRating = 
    {"succeeded": "true",
      "rating": [
        {
          "username": "test1",
          "score": 100
        },
        {
        "username": "test2",
        "score": 90
        },
        {
          "username": "test3",
          "score": 80
        },
        {
          "username": "test4",
          "score": 70
        }
      ]
    }
  

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RatingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    userService = jasmine.createSpyObj(['userService']);
    component = new RatingComponent(userService);
    const observable: Observable<Object> = of(testRating);
    userService.getRating = jasmine.createSpy().and.callFake(() => {
      return observable;
    });
    component.ngOnInit();
  });

  it('Сторінка повинна відображати отриманий рейтинг гравців (TC25)', () => {
    expect(component.userRating).toEqual(testRating);
  });

  /* it('should create', () => {
    expect(component).toBeTruthy();
  }); */
});
