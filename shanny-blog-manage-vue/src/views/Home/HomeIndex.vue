<script setup>
import { ref } from 'vue'
import NoteManage from './components/NoteManage.vue'
import ProjectManage from './components/ProjectManage.vue'
import ToolManage from './components/ToolManage.vue'
import MediaManage from './components/MediaManage.vue'
import BugManage from './components/BugManage.vue'
import BoardManage from './components/BoardManage.vue'
import TagManage from './components/TagManage.vue'
import InfoManage from './components/InfoManage.vue'
import AboutManage from './components/AboutManage.vue'
import CategoryManage from './components/CategoryManage.vue'
import { useAccountStore } from '@/stores/modules/account'
import { swal } from '@/utils/sweetalert'
const accountStore = useAccountStore()
const active = ref('note')
const logout = () => {
  swal('', '确定退出登陆吗？', '', 'question', true, true).then((result) => {
    if (result.isConfirmed) {
      accountStore.logout()
    }
  })
}
const goWebSite = () => {
  window.open('https://www.shanny.work', '_blank')
}
</script>
<template>
  <div class="drawer drawer-open">
    <input id="my-drawer-4" type="checkbox" class="drawer-toggle" checked />
    <div class="drawer-content w-full h-screen p-5">
      <div class="card bg-base-200 w-full h-full p-5">
        <div v-if="active == 'note'">
          <NoteManage />
        </div>
        <div v-else-if="active == 'project'">
          <ProjectManage />
        </div>
        <div v-else-if="active == 'tool'">
          <ToolManage />
        </div>
        <div v-else-if="active == 'media'">
          <MediaManage />
        </div>
        <div v-else-if="active == 'bug'">
          <BugManage />
        </div>
        <div v-else-if="active == 'board'">
          <BoardManage />
        </div>
        <div v-else-if="active == 'tag'">
          <TagManage />
        </div>
        <div v-else-if="active == 'info'">
          <InfoManage />
        </div>
        <div v-else-if="active == 'about'">
          <AboutManage />
        </div>
        <div v-else-if="active == 'category'">
          <CategoryManage />
        </div>
      </div>
    </div>
    <div class="drawer-side is-drawer-close:overflow-visible">
      <label
        for="my-drawer-4"
        aria-label="close sidebar"
        class="drawer-overlay"
      ></label>
      <div
        class="is-drawer-close:w-20 is-drawer-open:w-40 bg-base-200 flex flex-col items-start min-h-full"
      >
        <ul class="menu w-full grow">
          <li @click="goWebSite()">
            <button
              class="is-drawer-close:tooltip is-drawer-close:tooltip-right text-red"
              data-tip="WebSite"
            >
              <font-awesome-icon icon="fa-regular fa-circle-left" />
              <span class="is-drawer-close:hidden">WebSite</span>
            </button>
          </li>
          <li>
            <h2
              class="menu-title is-drawer-close:tooltip is-drawer-close:tooltip-right"
              data-tip="Article"
            >
              <font-awesome-icon icon="fa-solid fa-newspaper" />
              <span class="is-drawer-close:hidden ml-1">Article</span>
            </h2>
            <ul>
              <li @click="active = 'note'">
                <button
                  class="is-drawer-close:tooltip is-drawer-close:tooltip-right"
                  :class="{ 'active-menu': active == 'note' }"
                  data-tip="Note"
                >
                  <font-awesome-icon icon="fa-solid fa-note-sticky" />
                  <span class="is-drawer-close:hidden">Note</span>
                </button>
              </li>
              <li @click="active = 'project'">
                <button
                  class="is-drawer-close:tooltip is-drawer-close:tooltip-right"
                  :class="{ 'active-menu': active == 'project' }"
                  data-tip="Project"
                >
                  <font-awesome-icon icon="fa-solid fa-bars-progress" />
                  <span class="is-drawer-close:hidden">Project</span>
                </button>
              </li>
              <!-- <li @click="active = 'bug'">
                <button
                  class="is-drawer-close:tooltip is-drawer-close:tooltip-right"
                  :class="{ 'active-menu': active == 'bug' }"
                  data-tip="Bug"
                >
                  <font-awesome-icon icon="fa-solid fa-bug" />
                  <span class="is-drawer-close:hidden">Bug</span>
                </button>
              </li> -->
            </ul>
          </li>
          <li @click="active = 'tool'">
            <button
              class="is-drawer-close:tooltip is-drawer-close:tooltip-right"
              :class="{ 'active-menu': active == 'tool' }"
              data-tip="Tool"
            >
              <font-awesome-icon icon="fa-solid fa-screwdriver-wrench" />
              <span class="is-drawer-close:hidden">Tool</span>
            </button>
          </li>
          <!-- <li @click="active = 'media'">
            <button
              class="is-drawer-close:tooltip is-drawer-close:tooltip-right"
              :class="{ 'active-menu': active == 'media' }"
              data-tip="Media"
            >
              <font-awesome-icon icon="fa-solid fa-photo-film" />
              <span class="is-drawer-close:hidden">Media</span>
            </button>
          </li> -->
          <!-- <li @click="active = 'board'">
            <button
              class="is-drawer-close:tooltip is-drawer-close:tooltip-right"
              :class="{ 'active-menu': active == 'board' }"
              data-tip="Board"
            >
              <font-awesome-icon icon="fa-solid fa-envelope" />
              <span class="is-drawer-close:hidden">Board</span>
            </button>
          </li> -->

          <li>
            <h2
              class="menu-title is-drawer-close:tooltip is-drawer-close:tooltip-right"
              data-tip="Others"
            >
              <font-awesome-icon icon="fa-solid fa-list" />
              <span class="is-drawer-close:hidden ml-1">Others</span>
            </h2>
            <ul>
              <li @click="active = 'info'">
                <button
                  class="is-drawer-close:tooltip is-drawer-close:tooltip-right"
                  :class="{ 'active-menu': active == 'info' }"
                  data-tip="UserInfo"
                >
                  <font-awesome-icon icon="fa-solid fa-user" />
                  <span class="is-drawer-close:hidden">UserInfo</span>
                </button>
              </li>
              <li @click="active = 'about'">
                <button
                  class="is-drawer-close:tooltip is-drawer-close:tooltip-right"
                  :class="{ 'active-menu': active == 'about' }"
                  data-tip="About"
                >
                  <font-awesome-icon icon="fa-solid fa-circle-info" />
                  <span class="is-drawer-close:hidden">About</span>
                </button>
              </li>
              <li @click="active = 'category'">
                <button
                  class="is-drawer-close:tooltip is-drawer-close:tooltip-right"
                  :class="{ 'active-menu': active == 'category' }"
                  data-tip="Category"
                >
                  <font-awesome-icon icon="fa-solid fa-layer-group" />
                  <span class="is-drawer-close:hidden">Category</span>
                </button>
              </li>
              <li @click="active = 'tag'">
                <button
                  class="is-drawer-close:tooltip is-drawer-close:tooltip-right"
                  :class="{ 'active-menu': active == 'tag' }"
                  data-tip="Tag"
                >
                  <font-awesome-icon icon="fa-solid fa-tags" />
                  <span class="is-drawer-close:hidden">Tag</span>
                </button>
              </li>
            </ul>
          </li>
          <li @click="logout()">
            <button
              class="is-drawer-close:tooltip is-drawer-close:tooltip-right text-red"
              data-tip="Logout"
            >
              <font-awesome-icon icon="fa-solid fa-right-from-bracket" />
              <span class="is-drawer-close:hidden">Logout</span>
            </button>
          </li>
        </ul>
        <div
          class="m-2 is-drawer-close:tooltip is-drawer-close:tooltip-right"
          data-tip="Open"
        >
          <label
            for="my-drawer-4"
            class="btn btn-ghost btn-circle drawer-button is-drawer-open:rotate-y-180"
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              viewBox="0 0 24 24"
              stroke-linejoin="round"
              stroke-linecap="round"
              stroke-width="2"
              fill="none"
              stroke="currentColor"
              class="inline-block size-4 my-1.5"
            >
              <path
                d="M4 4m0 2a2 2 0 0 1 2 -2h12a2 2 0 0 1 2 2v12a2 2 0 0 1 -2 2h-12a2 2 0 0 1 -2 -2z"
              ></path>
              <path d="M9 4v16"></path>
              <path d="M14 10l2 2l-2 2"></path>
            </svg>
          </label>
        </div>
      </div>
    </div>
  </div>
</template>
<style lang="scss" scoped>
.active-menu {
  color: var(--menu-active-fg);
  background-color: var(--menu-active-bg);
}
</style>
