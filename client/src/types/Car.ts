export interface Car {
  firm: string,
  model: string,
  year: number,
  power: number,
  color: string,
  price: number,
}

export interface CarPreview extends Car {
  preview: string,
}
