// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.

// http://192.168.0.19:8000/api/
// http://192.168.0.23:8888/api/

export const environment = {
    production: false,
    SERVER_API_URL: "https://localhost:44300/",
    
    // site test
    //SSO_LOGIN_URL: "http://tone.posco.net/idms/webapps/jsp/one/one_redirect.jsp?redir_url=",
    // site live
    // SSO_LOGIN_URL: "http://one.posco.net/idms/webapps/jsp/one/one_redirect.jsp?redir_url=",
    //SSO_REDIRECT_URL: "http://localhost:50808/AuthSso/getLoginSso/"
  
    // SERVER_API_URL: "http://crm.atmaneuler.com/api/",
  
    // firebase: {},
  
  
    // debug: true,
    // log: {
    //   auth: false,
    //   store: false,
    // },
  
    // smartadmin: {
    //   api: null,
    //   db: 'smartadmin-angular'
    // }
  
  
  };
  