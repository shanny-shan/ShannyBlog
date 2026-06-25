<script setup>
import { onMounted, ref, computed } from 'vue'
import InfoDialog from '@/views/_components/dialog/InfoDialog.vue'
import { useAccountStore, usePageStore } from '@/stores'
import PaginationComponent from '@/views/_components/common/PaginationComponent.vue'

const accountStore = useAccountStore()
const pageStore = usePageStore()

const { pageList, totalPages } = pageStore.getPageData(() => accountStore.users)

const isSelectedAll = computed({
  get() {
    return pageStore.isAllRowsChecked(pageList, 'uuid')
  },
  set() {
    pageStore.toggleAllRows(pageList, 'uuid')
  },
})

onMounted(async () => {
  await accountStore.getAllUsers()
})
</script>
<template>
  <div>
    <div class="flex justify-end">
      <button
        class="btn btn-warning"
        :disabled="pageStore.selectedIds.length === 0"
        @click="accountStore.deleteUsers()"
      >
        Delete
      </button>
      <!-- <button class="btn btn-primary" @click="adminStore.openDialog('info')">
        Add User
      </button> -->
    </div>
    <div class="overflow-x-auto mt-10">
      <table class="table">
        <!-- head -->
        <thead>
          <tr>
            <th>
              <label>
                <input
                  type="checkbox"
                  class="checkbox"
                  v-model="isSelectedAll"
                />
              </label>
            </th>
            <th>UserId</th>
            <th>nickName</th>
            <th>userName</th>
            <th>birthday</th>
            <th>sex</th>
            <th>mobile</th>
            <th>lastLogin</th>
            <th>other</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in pageList" :key="item.uuid">
            <th>
              <label>
                <input
                  type="checkbox"
                  class="checkbox"
                  :value="item.uuid"
                  v-model="pageStore.selectedIds"
                />
              </label>
            </th>
            <td>
              <div class="flex items-center gap-3">
                <!-- <div class="avatar">
                  <div class="mask mask-squircle h-12 w-12">
                    <img
                      src="@/assets/images/avatar.jpg"
                      alt="Avatar Tailwind CSS Component"
                    />
                  </div>
                </div> -->
                <div>
                  <div class="font-bold">{{ item.userId }}</div>
                  <div class="text-sm opacity-50">{{ item.status }}</div>
                </div>
              </div>
            </td>
            <td>{{ item.userDetails?.nickname }}</td>
            <td>{{ item.userDetails?.username }}</td>
            <td>{{ item.userDetails?.birthday?.substring(0, 10) }}</td>
            <td>{{ item.userDetails?.sex }}</td>
            <td>{{ item.mobile }}</td>
            <td>{{ item.lastLoginTime?.substring(0, 10) }}</td>
            <th>
              <div class="flex gap-2">
                <button
                  class="btn btn-ghost btn-xs"
                  @click="accountStore.openEditInfo(item)"
                >
                  Edit
                </button>
                <button
                  class="btn btn-ghost btn-xs"
                  @click="accountStore.deleteInfo(item)"
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
  <InfoDialog />
</template>
<style lang="scss" scoped></style>
