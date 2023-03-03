import { Component, OnInit } from '@angular/core';
import { CategoryDto, MainCategoryPostDto } from 'src/app/dto/main-category.dto';
import { TagDto } from 'src/app/dto/tag.dto';
import { MainCategoriesService } from 'src/app/services/main-categories-service/main-categories.service';

@Component({
  selector: 'app-post-ad',
  templateUrl: './post-ad.component.html',
  styleUrls: ['./post-ad.component.scss']
})
export class PostAdComponent implements OnInit {

  mainCategories: MainCategoryPostDto[] = [];

  category!: CategoryDto;
  tags: TagDto[] = [];

  constructor(private mainCategoriesService: MainCategoriesService) { }

  ngOnInit(): void {
    this.mainCategoriesService.getAllForPosting().subscribe(mcList => this.mainCategories = mcList);

    this.tags = [
      new TagDto(23, 'Price', true, 'input', null, 'price'),
      new TagDto(24, 'Currency', true, 'dropdown', ['uah', 'eur', 'usd'], 'price'),
      new TagDto(25, 'Payment per', true, 'dropdown', ['Hour', 'Night', 'Week', 'Month'], 'price'),
      new TagDto(26, 'Contract price', true, 'switch', null, 'price'),
      new TagDto(1, 'Private person or business', true, 'pills', ['Private person', 'Business']),
      new TagDto(2, 'Additional information (Rent)', false, 'checkbox', ['No commission', 'Ready to cooperate with realtors', 'For shared rental']),
      new TagDto(3, 'Distance to nearest city', false, 'dropdown', ['In the city', 'Up to 5 km', 'Up to 10 km', 'Up to 20 km', 'Up to 50 km', 'Up to 100 km']),
      new TagDto(4, 'Number of floors', true, 'input', null, 'number'),
      new TagDto(5, 'Total area', true, 'input', null, 'number m2'),
      new TagDto(6, 'Number of rooms', true, 'input', null, 'number'),
      new TagDto(7, 'Land area', true, 'input', null, 'number acres'),
      new TagDto(8, 'House type', true, 'dropdown', ['House', 'Club house', 'Cottage', 'Townhouse', 'Country house']),
      new TagDto(9, 'Year of construction / commissioning', false, 'input', null, 'number year'),
      new TagDto(10, 'Wall type', false, 'dropdown', ['Brick', 'Panel', 'Monolit', 'Wood', 'Other']),
      new TagDto(11, 'External wall insulation', false, 'dropdown', ['Mineral wool', 'Basalt', 'Polystyrene']),
      new TagDto(12, 'Bathroom', false, 'dropdown', ['Separate', 'Adjacent', 'In the yard', '2 or more']),
      new TagDto(13, 'Heating', false, 'dropdown', ['Centralized', 'Individual gas', 'Individual electric', 'Solid fuel', 'Combined', 'Own boiler house', 'Other']),
      new TagDto(14, 'Renovation', false, 'dropdown', ['Euro renovation', "Author's project", 'Redecoration', 'Living condition', 'After builders']),
      new TagDto(15, 'Furnishings', false, 'pills', ['With furniture', 'Without furniture']),
      new TagDto(16, 'Appliances', false, 'checkbox', ['Fridge', 'Electric kettle', 'Coffee machine', 'Dishwasher', 'Hair dryer', 'Washing machine', 'Stove',
        'Drying machine', 'Hob', 'Iron', 'Oven', 'Vacuum cleaner', 'Microwave', 'Multicooker', 'Without appliances']),
      new TagDto(17, 'Multimedia', false, 'checkbox', ['Wi-Fi', 'TV', 'Internet', 'Without multimedia']),
      new TagDto(18, 'Comfort', false, 'checkbox', ['Air conditioning', 'Electric gates', 'Floor heating', 'Guest house', 'Bathtub', 'Gazebo/Grill',
        'Shower cabin', 'Balcony', 'Gym', 'Terrace', 'Sauna/Bathhouse', 'Fireplace', 'Swimming pool', 'Garden', 'Alarm system', 'Basement', 'Fencing', 'Garage']),
      new TagDto(19, 'Communications', false, 'checkbox', ['Gas', 'Sewage', 'Waste disposal', 'Central water supply', 'Borehole', 'Asphalt road', 'Electricity', 'Without communications']),
      new TagDto(20, 'Infrastructure nearby', false, 'checkbox', ['Kindergarten', 'Pharmacy', 'School', 'Hospital', 'City center', 'Bus stop', 'Restaurant/Cafe',
        'Subway', 'Cinema', 'Market', 'Post office', 'Shop', 'Bank', 'Supermarket', 'Park', 'Railway station', 'Playground']),
      new TagDto(21, 'Landscape nearby', false, 'checkbox', ['River', 'Hills', 'Reservoir', 'Mountains', 'Lake', 'Sea', 'Forest', 'Islands']),
      new TagDto(22, 'Additionally (Rent)', false, 'checkbox', ['Only girls', 'Only boys', 'Only families', 'Allowed students', 'Allowed foreigners',
        'Allowed with kids', 'With hosts', 'Smoking allowed', 'Animals allowed'])
    ];
  }

  onCategoryClick = (category: CategoryDto) => this.category = category;
}