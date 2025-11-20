export const CREATE_NOTE = `
  mutation($title:String!,$content:String){
    createNote(title:$title, content:$content){ title content }
  }
`;
export const LOGIN = `
  mutation($email:String!,$secret:String!){
    login(email:$email, secret:$secret)
  }
`;
