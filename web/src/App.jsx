import { useState } from 'react'
import { gql } from './api/axios'
import { LOGIN, CREATE_NOTE } from './graphql/mutations'
import { MY_NOTES } from './graphql/queries'

export default function App(){
  const [email, setEmail] = useState('demo@mail.com')
  const [title, setTitle] = useState('Mi primera nota')
  const [content, setContent] = useState('Contenido')
  const [notes, setNotes] = useState([])

  const doLogin = async () => {
    const data = await gql(`mutation($email:String!,$secret:String!){ login(email:$email,secret:$secret) }`, { email, secret: import.meta.env.VITE_JWT_SECRET || 'dev_secret' })
    localStorage.setItem('token', data.login)
    alert('Token guardado en localStorage')
  }

  const addNote = async () => {
    await gql(CREATE_NOTE, { title, content })
    const data = await gql(MY_NOTES)
    setNotes(data.myNotes)
  }

  return (
    <div style={{padding:24}}>
      <h1>Librería Web (React + Axios + GraphQL)</h1>
      <p>API GraphQL: {import.meta.env.VITE_GRAPHQL_URL || 'http://localhost:5000/graphql'}</p>
      <div>
        <h3>Login (demo)</h3>
        <input value={email} onChange={e=>setEmail(e.target.value)} placeholder='email' />
        <button onClick={doLogin}>Login</button>
      </div>
      <div style={{marginTop:12}}>
        <h3>Crear Nota</h3>
        <input value={title} onChange={e=>setTitle(e.target.value)} placeholder='título' />
        <input value={content} onChange={e=>setContent(e.target.value)} placeholder='contenido' />
        <button onClick={addNote}>Crear y listar</button>
      </div>
      <ul>
        {notes.map((n,i)=>(<li key={i}><b>{n.title}</b>: {n.content}</li>))}
      </ul>
    </div>
  )
}
