
const baseUrl = '/';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'ExtensionsManagement',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://admin.t-social.tpos.dev',
    redirectUri: baseUrl,
    clientId: 'ExtensionsManagement_App',
    responseType: 'code',
    scope: 'offline_access ExtensionsManagement',
    requireHttps: true
  },
  AccountApi : {
    accountApi : 'https://account.tpos.dev/'
  },
  apis: {
    default: {
      url: 'https://admin.t-social.tpos.dev',
      rootNamespace: 'ExtensionsManagement',
    },
  },
  apiActivity: {
    default: {
      url: 'https://admin.t-social.tpos.dev',
    },
  },
}
