import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"


class CultMembersService {
  async joinCult(cultMember) {
    const res = await api.post('api/cultMembers', cultMember)
    logger.log('[Join CUlt]', res.data)
    AppState.cultists.unshift(res.data)
  }

  async removeMember(cultMemberId) {
    const res = await api.delete('api/cultMembers/' + cultMemberId)
    logger.log('[Remove Member]', res.data)
    AppState.cultists = AppState.cultists.filter(c => c.cultMemberId != cultMemberId)
  }
}

export const cultMemberService = new CultMembersService()
