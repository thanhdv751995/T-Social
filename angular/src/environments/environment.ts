
const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'ExtensionsManagement',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44352',
    redirectUri: baseUrl,
    clientId: 'ExtensionsManagement_App',
    responseType: 'code',
    scope: 'offline_access ExtensionsManagement',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44352',
      rootNamespace: 'ExtensionsManagement',
    },
  },
  AccountApi : {
    accountApi : 'https://account.tpos.dev/'
  },
  apiActivity: {
    default: {
      url: 'https://localhost:44338',
    },
  },
}
