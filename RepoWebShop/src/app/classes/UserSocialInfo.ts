export class UserSocialInfo {
    displayName: string;
    email: string;
    photoURL: string;
    providerId: string;
    uid: string;
    constructor(user: firebase.User, provider: firebase.UserInfo) {
        this.email = provider.email ? provider.email : user.email;
        this.displayName = provider.displayName;
        this.photoURL = provider.photoURL;
        this.uid = provider.uid;
        switch (provider.providerId) {
        case 'facebook.com':
            this.providerId = 'facebook';
            break;
        case 'google.com':
            this.providerId = 'google';
            break;
        default:
            this.providerId = provider.providerId;
        }
    }
}
