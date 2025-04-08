declare module server {
    enum RegistrationType {
        Registration = "Registration",
        LateRegistration = "LateRegistration",
        Reregistration = "Reregistration"
    }

    interface RegistrationObject {
        type: RegistrationType;
        race: string;
        startNo: string;
        team: string;
        position1?: string;
        position2?: string;
        position3?: string;
        position4?: string;
        position5?: string;
        position6?: string;
        position7?: string;
        position8?: string;
        positionCox?: string;
        chairMan: string;
        requestedAt: Date;
    }
}