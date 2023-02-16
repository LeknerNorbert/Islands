using Islands.DTOs;
using Islands.Exceptions;
using Islands.Models;
using Islands.Models.Enums;
using Islands.Repositories.ClassifiedAdRepository;
using Islands.Repositories.PlayerInformationRepository;

namespace Islands.Services.ClassifiedAdService
{
    public class AdService : IAdService
    {
        private readonly IAdRepository _adRepository;
        private readonly IPlayerRepository _playerRepository;

        public AdService(IAdRepository adRepository, IPlayerRepository playerRepository)
        {
            _adRepository = adRepository;
            _playerRepository = playerRepository; 
        }

        public async Task AddAsync(string username, NewAdDTO newAd)
        {
            try
            {
                Player advertiser = await _playerRepository.GetByUsernameAsync(username);

                switch (newAd.Item)
                {
                    case Item.Coin:
                        if (advertiser.Coins >= newAd.Amount)
                        {
                            advertiser.Coins -= newAd.Amount;
                        }
                        else
                        {
                            throw new InsufficientItemsException();
                        }

                        break;
                    case Item.Wood:
                        if (advertiser.Woods >= newAd.Amount)
                        {
                            advertiser.Woods -= newAd.Amount;
                        }
                        else
                        {
                            throw new InsufficientItemsException();
                        }

                        break;
                    case Item.Stone:
                        if (advertiser.Stones >= newAd.Amount)
                        {
                            advertiser.Stones -= newAd.Amount;
                        }
                        else
                        {
                            throw new InsufficientItemsException();
                        }

                        break;
                    case Item.Iron:
                        if (advertiser.Irons >= newAd.Amount)
                        {
                            advertiser.Irons -= newAd.Amount;
                        }
                        else
                        {
                            throw new InsufficientItemsException();
                        }

                        break;
                }

                Ad classifiedAd = new()
                {
                    Item = newAd.Item,
                    Amount = newAd.Amount,
                    ReplacementItem = newAd.ReplacementItem,
                    ReplacementAmount = newAd.ReplacementAmount,
                    PublishDate = DateTime.Now,
                    PlayerInformation = advertiser
                };

                await _adRepository.AddAsync(classifiedAd);
                await _playerRepository.UpdateAsync(advertiser);
            }
            catch (Exception ex)
            {
                if (ex is InsufficientItemsException)
                {
                    throw new InsufficientItemsException("There are not enough items.");
                }

                throw new ServiceException("Failed to create ad.", ex);
            }
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                Ad classifiedAd = await _adRepository.GetByIdAsync(id);
                await _adRepository.RemoveAsync(classifiedAd);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Failed to remove ad.", ex);
            }
        
        }

        public async Task<List<AdDTO>> GetAllAsync()
        {
            try
            {
                return await _adRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new ServiceException("Failed to get ads.", ex);
            }
        }

        public async Task<List<AdDTO>> GetAllByUsernameAsync(string username)
        {
            try
            {
                return await _adRepository.GetAllByUsernameAsync(username);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Failed to get ads.", ex);
            }
        }

        public async Task BuyAsync(string customerUsername, int id)
        {
            try
            {
                Ad ad = await _adRepository.GetByIdAsync(id);
                Player buyer = await _playerRepository.GetByUsernameAsync(customerUsername);
                Player advertiser = await _playerRepository.GetByForAd(id);

                switch (ad.ReplacementItem)
                {
                    case Item.Coin:
                        if (buyer.Coins >= ad.ReplacementAmount)
                        {
                            buyer.Coins -= ad.ReplacementAmount;
                            advertiser.Coins += ad.ReplacementAmount;
                        }
                        else
                        {
                            throw new InsufficientItemsException();
                        }

                        break;
                    case Item.Wood:
                        if (buyer.Woods >= ad.ReplacementAmount)
                        {
                            buyer.Woods -= ad.ReplacementAmount;
                            advertiser.Woods += ad.ReplacementAmount;
                        }
                        else
                        {
                            throw new InsufficientItemsException();
                        }

                        break;
                    case Item.Stone:
                        if (buyer.Stones >= ad.ReplacementAmount)
                        {
                            buyer.Stones -= ad.ReplacementAmount;
                            advertiser.Stones += ad.ReplacementAmount;
                        }
                        else
                        {
                            throw new InsufficientItemsException();
                        }

                        break;
                    case Item.Iron:
                        if (buyer.Irons >= ad.ReplacementAmount)
                        {
                            buyer.Irons -= ad.ReplacementAmount;
                            advertiser.Irons += ad.ReplacementAmount;
                        }
                        else
                        {
                            throw new InsufficientItemsException();
                        }

                        break;
                }

                switch (ad.Item)
                {
                    case Item.Coin:
                        buyer.Coins += ad.Amount;

                        break;
                    case Item.Wood:
                        buyer.Woods += ad.Amount;

                        break;
                    case Item.Stone:
                        buyer.Stones += ad.Amount;

                        break;
                    case Item.Iron:
                        buyer.Irons += ad.Amount;

                        break;
                }

                await _playerRepository.UpdateAsync(buyer);
                await _playerRepository.UpdateAsync(advertiser);
                await _adRepository.RemoveAsync(ad);
            }
            catch (Exception ex)
            {
                if (ex is InsufficientItemsException)
                {
                    throw new InsufficientItemsException("There are not enough items.");
                }

                throw new ServiceException("Failed to buy ad.", ex);
            }
        }
    }
}
