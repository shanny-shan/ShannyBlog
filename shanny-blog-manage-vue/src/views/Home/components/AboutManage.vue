<script setup>
import AboutDialog from '@/views/_components/dialog/AboutDialog.vue'
import { onMounted } from 'vue'
import { useAdminStore, useAboutStore, usePageStore } from '@/stores'
import PaginationComponent from '@/views/_components/common/PaginationComponent.vue'
const adminStore = useAdminStore()
const aboutStore = useAboutStore()
const pageStore = usePageStore()

const { pageList, totalPages } = pageStore.getPageData(
  () => aboutStore.aboutList,
)

onMounted(async () => {
  await aboutStore.getAboutList()
})
</script>
<template>
  <div>
    <div class="flex justify-end gap-2">
      <button class="btn btn-primary" @click="adminStore.openDialog('about')">
        Add About
      </button>
    </div>
    <div class="overflow-x-auto mt-2">
      <table class="table">
        <thead>
          <tr>
            <th>
              <label>
                <input type="checkbox" class="checkbox" />
              </label>
            </th>
            <th>Name</th>
            <th>Tag</th>
            <th>Introduce</th>
            <th>GitHub</th>
            <!-- <th>Steam</th> -->
            <th>Web</th>
            <!-- <th>Bilibili</th> -->
            <th>IsActive</th>
            <!-- <th>CreateTime</th> -->
            <th>UpdateTime</th>
            <th>Edit</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in pageList" :key="item.id">
            <th>
              <label>
                <input type="checkbox" class="checkbox" />
              </label>
            </th>
            <td>{{ item.name }}</td>
            <td>
              {{
                item.tag.length > 5 ? item.tag.slice(0, 5) + '...' : item.tag
              }}
            </td>
            <td>
              {{
                item.introduce.length > 10
                  ? item.introduce.slice(0, 10) + '...'
                  : item.introduce
              }}
            </td>
            <td>{{ item.github }}</td>
            <!-- <td>{{ item.steam }}</td> -->
            <td>{{ item.web }}</td>
            <!-- <td>{{ item.biliBili }}</td> -->
            <td>{{ item.isActive }}</td>
            <!-- <td>{{ item.createTime }}</td> -->
            <td>{{ item.updateTime.substring(0, 10) }}</td>
            <th>
              <div class="flex gap-2">
                <button
                  class="btn btn-ghost btn-xs"
                  @click="aboutStore.openEditAbout(item)"
                >
                  Edit
                </button>
                <button
                  class="btn btn-ghost btn-xs"
                  @click="aboutStore.deleteAbout(item)"
                >
                  Delete
                </button>
              </div>
            </th>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="mt-2 md:mt-10 flex justify-center" v-if="totalPages > 1">
      <PaginationComponent
        :current-page="pageStore.currentPage"
        :total-pages="totalPages"
        :page-range="5"
        @page-change="pageStore.handlePageChange"
      />
    </div>
  </div>
  <AboutDialog />
</template>
<style lang="scss" scoped></style>
