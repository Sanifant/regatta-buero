export enum RegistrationType {
    Registration = "Registration",
    LateRegistration = "LateRegistration",
    Reregistration = "Reregistration"
  }

export class Registration {

    constructor() {}

    type: RegistrationType = RegistrationType.Registration;
    race: string = "";
    startNo: string = "";
    team: string = "";
    chairMan: string = "";
    position1?: string;
    position2?: string;
    position3?: string;
    position4?: string;
    position5?: string;
    position6?: string;
    position7?: string;
    position8?: string;
    positionCox?: string;
  }