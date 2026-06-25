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
import { useAccountStore, useSiteStore } from '@/stores'
const accountStore = useAccountStore()
const siteStore = useSiteStore()
</script>
<template>
  <div class="drawer">
    <input
      id="my-drawer-4"
      type="checkbox"
      class="drawer-toggle"
      v-model="siteStore.drawerOpen"
    />

    <div class="drawer-content w-full h-screen p-0">
      <label
        for="my-drawer-4"
        class="btn btn-ghost drawer-button mb-4 inline-flex items-center gap-2 absolute top-5 left-5 z-1"
      >
        <svg
          xmlns="http://www.w3.org/2000/svg"
          viewBox="0 0 24 24"
          stroke-linejoin="round"
          stroke-linecap="round"
          stroke-width="2"
          fill="none"
          stroke="currentColor"
          class="size-4"
        >
          <path d="M4 6h16M4 12h16M4 18h7" />
        </svg>
      </label>

      <div class="card bg-base-200 w-full min-h-full p-5">
        <div v-if="siteStore.active == 'note'">
          <NoteManage />
        </div>
        <div v-else-if="siteStore.active == 'project'">
          <ProjectManage />
        </div>
        <div v-else-if="siteStore.active == 'tool'">
          <ToolManage />
        </div>
        <div v-else-if="siteStore.active == 'media'">
          <MediaManage />
        </div>
        <div v-else-if="siteStore.active == 'bug'">
          <BugManage />
        </div>
        <div v-else-if="siteStore.active == 'board'">
          <BoardManage />
        </div>
        <div v-else-if="siteStore.active == 'tag'">
          <TagManage />
        </div>
        <div v-else-if="siteStore.active == 'info'">
          <InfoManage />
        </div>
        <div v-else-if="siteStore.active == 'about'">
          <AboutManage />
        </div>
        <div v-else-if="siteStore.active == 'category'">
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
        class="is-drawer-close:w-50 is-drawer-open:w-50 bg-base-200 flex flex-col items-start min-h-full"
      >
        <ul class="menu w-full grow">
          <li @click="siteStore.goWebSite()">
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
              <li @click="siteStore.changeHeaderM('note')">
                <button
                  class="is-drawer-close:tooltip is-drawer-close:tooltip-right"
                  :class="{ 'active-menu': siteStore.active == 'note' }"
                  data-tip="Note"
                >
                  <font-awesome-icon icon="fa-solid fa-note-sticky" />
                  <span class="is-drawer-close:hidden">Note</span>
                </button>
              </li>
              <li @click="siteStore.changeHeaderM('project')">
                <button
                  class="is-drawer-close:tooltip is-drawer-close:tooltip-right"
                  :class="{ 'active-menu': siteStore.active == 'project' }"
                  data-tip="Project"
                >
                  <font-awesome-icon icon="fa-solid fa-bars-progress" />
                  <span class="is-drawer-close:hidden">Project</span>
                </button>
              </li>
            </ul>
          </li>
          <li @click="siteStore.changeHeaderM('tool')">
            <button
              class="is-drawer-close:tooltip is-drawer-close:tooltip-right"
              :class="{ 'active-menu': siteStore.active == 'tool' }"
              data-tip="Tool"
            >
              <font-awesome-icon icon="fa-solid fa-screwdriver-wrench" />
              <span class="is-drawer-close:hidden">Tool</span>
            </button>
          </li>
          <li>
            <h2
              class="menu-title is-drawer-close:tooltip is-drawer-close:tooltip-right"
              data-tip="Others"
            >
              <font-awesome-icon icon="fa-solid fa-list" />
              <span class="is-drawer-close:hidden ml-1">Others</span>
            </h2>
            <ul>
              <li @click="siteStore.changeHeaderM('info')">
                <button
                  class="is-drawer-close:tooltip is-drawer-close:tooltip-right"
                  :class="{ 'active-menu': siteStore.active == 'info' }"
                  data-tip="UserInfo"
                >
                  <font-awesome-icon icon="fa-solid fa-user" />
                  <span class="is-drawer-close:hidden">UserInfo</span>
                </button>
              </li>
              <li @click="siteStore.changeHeaderM('about')">
                <button
                  class="is-drawer-close:tooltip is-drawer-close:tooltip-right"
                  :class="{ 'active-menu': siteStore.active == 'about' }"
                  data-tip="About"
                >
                  <font-awesome-icon icon="fa-solid fa-circle-info" />
                  <span class="is-drawer-close:hidden">About</span>
                </button>
              </li>
              <li @click="siteStore.changeHeaderM('category')">
                <button
                  class="is-drawer-close:tooltip is-drawer-close:tooltip-right"
                  :class="{ 'active-menu': siteStore.active == 'category' }"
                  data-tip="Category"
                >
                  <font-awesome-icon icon="fa-solid fa-layer-group" />
                  <span class="is-drawer-close:hidden">Category</span>
                </button>
              </li>
              <li @click="siteStore.changeHeaderM('tag')">
                <button
                  class="is-drawer-close:tooltip is-drawer-close:tooltip-right"
                  :class="{ 'active-menu': siteStore.active == 'tag' }"
                  data-tip="Tag"
                >
                  <font-awesome-icon icon="fa-solid fa-tags" />
                  <span class="is-drawer-close:hidden">Tag</span>
                </button>
              </li>
            </ul>
          </li>
          <li @click="accountStore.logout()">
            <button
              class="is-drawer-close:tooltip is-drawer-close:tooltip-right text-red"
              data-tip="Logout"
            >
              <font-awesome-icon icon="fa-solid fa-right-from-bracket" />
              <span class="is-drawer-close:hidden">Logout</span>
            </button>
          </li>
        </ul>

        <div class="m-2 is-drawer-close:tooltip is-drawer-close:tooltip-right">
          <label
            for="my-drawer-4"
            class="btn btn-ghost btn-circle drawer-button is-drawer-open:rotate-y-180 transition-transform"
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
