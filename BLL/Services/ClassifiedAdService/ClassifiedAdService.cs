using BLL.DTOs;
using BLL.Exceptions;
using DAL.Models;
using DAL.Models.Enums;
using DAL.Repositories.ClassifiedAdRepository;
using DAL.Repositories.PlayerInformationRepository;
using DAL.Repositories.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.ClassifiedAdService
{
    public class ClassifiedAdService : IClassifiedAdService
    {
        private readonly IClassifiedAdRepository _classifiedAdRepository;
        private readonly IPlayerInformationRepository _playerInformationRepository;

        public ClassifiedAdService(IClassifiedAdRepository classifiedAdRepository, IPlayerInformationRepository playerInformationRepository)
        {
            _classifiedAdRepository = classifiedAdRepository;
            _playerInformationRepository = playerInformationRepository;
        }

        public void CreateClassifiedAd(string username, CreateClassifiedAdDto createClassifiedAd)
        {
            PlayerInformation playerInformation = _playerInformationRepository.GetPlayerInformation(username);

            switch (createClassifiedAd.Item)
            {
                case ItemType.Coin:
                    if (playerInformation.Coins >= createClassifiedAd.Amount)
                    {
                        playerInformation.Coins -= createClassifiedAd.Amount;
                    } 
                    else
                    {
                        throw new NotEnoughItemsException("There are not enough items to create classified ad.");
                    }

                    break;
                case ItemType.Wood:
                    if (playerInformation.Woods >= createClassifiedAd.Amount)
                    {
                        playerInformation.Woods -= createClassifiedAd.Amount;
                    }
                    else
                    {
                        throw new NotEnoughItemsException("There are not enough items to create classified ad.");
                    }

                    break;
                case ItemType.Stone:
                    if (playerInformation.Stones >= createClassifiedAd.Amount)
                    {
                        playerInformation.Stones -= createClassifiedAd.Amount;
                    }
                    else
                    {
                        throw new NotEnoughItemsException("There are not enough items to create classified ad.");
                    }

                    break;
                case ItemType.Iron:
                    if (playerInformation.Irons >= createClassifiedAd.Amount)
                    {
                        playerInformation.Irons -= createClassifiedAd.Amount;
                    }
                    else
                    {
                        throw new NotEnoughItemsException("There are not enough items to create classified ad.");
                    }

                    break;
            }

            ClassifiedAd classifiedAd = new()
            {
                Item = createClassifiedAd.Item,
                Amount = createClassifiedAd.Amount,
                ReplacementItem = createClassifiedAd.ReplacementItem,
                ReplacementAmount = createClassifiedAd.ReplacementAmount,
                PublishDate = DateTime.Now,
            };

            _playerInformationRepository.UpdatePlayerInformation(playerInformation);
            _classifiedAdRepository.CreateClassifiedAd(classifiedAd);
        }

        public void DeleteClassifiedAd(int id)
        {
            ClassifiedAd classifiedAd = _classifiedAdRepository.GetClassifiedAd(id);
            PlayerInformation? ownerPlayer = classifiedAd.PlayerInformation;

            switch (classifiedAd.Item)
            {
                case ItemType.Coin:
                    if (ownerPlayer != null)
                    {
                        ownerPlayer.Coins += classifiedAd.Amount;
                    }

                    break;
                case ItemType.Wood:
                    if (ownerPlayer != null)
                    {
                        ownerPlayer.Woods += classifiedAd.Amount;
                    }

                    break;
                case ItemType.Stone:
                    if (ownerPlayer != null)
                    {
                        ownerPlayer.Stones += classifiedAd.Amount;
                    }

                    break;
                case ItemType.Iron:
                    if (ownerPlayer != null)
                    {
                        ownerPlayer.Irons += classifiedAd.Amount;
                    }

                    break;
            }

            _classifiedAdRepository.DeleteClassifiedAd(classifiedAd);
            _playerInformationRepository.UpdatePlayerInformation(ownerPlayer);

        }

        public List<ClassifiedAdDto> GetClassifiedAds()
        {
            throw new NotImplementedException();
        }

        public List<ClassifiedAdDto> GetMyClassifiedAds(string username)
        {
            throw new NotImplementedException();
        }
    }
}
