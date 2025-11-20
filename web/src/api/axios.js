import axios from 'axios'

const api = axios.create({
  baseURL: (import.meta.env.VITE_GRAPHQL_URL || 'http://localhost:5000/graphql').replace('/graphql','')
})

api.interceptors.request.use(cfg => {
  const t = localStorage.getItem('token')
  if(t) cfg.headers.Authorization = `Bearer ${t}`
  return cfg
})

export const gql = async (query, variables) => {
  const res = await api.post('/graphql', { query, variables })
  if(res.data.errors?.length) throw new Error(res.data.errors[0].message)
  return res.data.data
}

export default api
