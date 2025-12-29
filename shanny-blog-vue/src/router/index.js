import { createRouter, createWebHistory } from 'vue-router'
const Layout = () => import('@/views/Layout/LayoutIndex.vue')
const Home = () => import('@/views/Home/HomeIndex.vue')
const Note = () => import('@/views/Note/NoteIndex.vue')
const NoteDetail = () => import('@/views/Note/NoteDetail.vue')
const Project = () => import('@/views/Project/ProjectIndex.vue')
const ProjectDetail = () => import('@/views/Project/ProjectDetail.vue')
const Tool = () => import('@/views/Tool/ToolIndex.vue')
const Media = () => import('@/views/Media/MediaIndex.vue')
const Bug = () => import('@/views/Bug/BugIndex.vue')
const Board = () => import('@/views/Board/BoardIndex.vue')

const routes = [
  {
    path: '',
    component: Layout,
    children: [
      {
        path: '',
        component: Home,
      },
      {
        path: '/article/note',
        component: Note,
      },
      {
        path: '/article/note/:id',
        component: NoteDetail,
      },
      {
        path: '/article/project',
        component: Project,
      },
      {
        path: '/article/project/:id',
        component: ProjectDetail,
      },
      {
        path: '/tool',
        component: Tool,
      },
      {
        path: '/media/:type',
        component: Media,
      },
      {
        path: '/article/bug',
        component: Bug,
      },
      {
        path: '/board',
        component: Board,
      },
    ],
  },
]
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
})

export default router
