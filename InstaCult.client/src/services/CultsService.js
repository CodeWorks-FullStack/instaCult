import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"


class CultsService {

  async getCults() {
    const res = await api.get('api/cults')
    logger.log('[Get Cults]', res.data)
    AppState.cults = res.data
  }

  async getById(id) {
    const res = await api.get('api/cults/' + id)
    logger.log('[Get one Cult]', res.data)
    AppState.activeCult = res.data
  }

  async getCultMembers(id) {
    const res = await api.get(`api/cults/${id}/members`)
    logger.log('[Get Cult Members]', res.data)
    AppState.cultists = res.data
  }

}

export const cultsService = new CultsService()
